﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ProductLib;

namespace GettingReal
{
    public class Menu
    {
        private Controller control;
        public Menu()
        {
            Console.WriteLine("Sætter op...");
            try
            {
                control = new Controller();
                Console.WriteLine("Færdig...");
                ShowMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("\nKan ikke fortsætte på grund af fejl!");
                Console.Write("Vil du åbne fejlloggen? (Y/N): ");
                string choice = Console.ReadLine().ToLower();
                if (choice == "y")
                {
                    System.Diagnostics.Process.Start("errorLog.txt");
                }
            }
            
        }

        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Per Olsen Automobiler - Lagersystem\n");
                Console.WriteLine("\t1. Bestilling");
                Console.WriteLine("\t2. Lager");
                Console.WriteLine("\t3. Reservedele");
                Console.WriteLine("\n\t0. Afslut");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        OrderMenu();
                        break;

                    case "2":
                        StorageMenu();
                        break;

                    case "3":
                        ProductMenu();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Dette er ikke en valgmulighed");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        void OrderMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Per Olsen Automobiler - Bestilling\n");
                Console.WriteLine("\t1. Indskriv bestilt produkt");
                Console.WriteLine("\t2. Se aktive ordre");
                Console.WriteLine("\t3. Afslut ordre");
                Console.WriteLine("\n\t0. Tilbage");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProductOrdered();
                        break;

                    case "2":
                        ActiveOrders();
                        Console.WriteLine("\nTryk på en knap for at vende tilbage...");
                        Console.ReadKey(true);
                        break;

                    case "3":
                        FinishOrder();
                        Console.WriteLine("\nTryk på en knap for at vende tilbage...");
                        Console.ReadKey(true);
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Dette er ikke en valgmulighed");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        void StorageMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Per Olsen Automobiler - Lager\n");
                Console.WriteLine("\t1. Tjek lagerbeholding");
                Console.WriteLine("\t2. Søg Lager");
                Console.WriteLine("\t3. Opdater produkt antal");
                Console.WriteLine("\n\t0. Tilbage");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllProducts();
                        Console.WriteLine("\nTryk på en knap for at vende tilbage...");
                        Console.ReadKey(true);
                        break;

                    case "2":
                        SearchProducts();
                        break;

                    case "3":
                        UpdateNumberOfProducts();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Dette er ikke en valgmulighed");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        void ProductMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Per Olsen Automobiler - Reservedele\n");
                Console.WriteLine("\t1. Opret produkt");
                Console.WriteLine("\t2. Slet produkt");
                Console.WriteLine("\n\t0. Tilbage");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateProduct();
                        break;

                    case "2":
                        RemoveProduct();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Dette er ikke en valgmulighed");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        void ActiveOrders()
        {
            Console.Clear();
            Console.WriteLine("ID\t" + Utility.LengthenString("Reservedelsnavn") + "\tOrdre Dato");
            List<Order> orders = control.GetOrders();
            foreach (Order o in orders)
            {
                if (o.Aktiv)
                {
                    Console.WriteLine(o.OrderId + "\t" + Utility.LengthenString(o.products[0].Navn) + "\t" + o.Dato);
                    for (int i = 1; i < o.products.Count; i++)
                    {
                        Console.WriteLine("\t" + Utility.LengthenString(o.products[i].Navn));
                    }
                    Console.WriteLine();
                }
            }
        }

        void FinishOrder()
        {
            ActiveOrders();
            Console.Write("Skriv ID'et på ordren der skal afsluttes(0 for tilbage): ");
            string orderId = Console.ReadLine();
            bool idParse = int.TryParse(orderId, out int id);
            if (idParse && id != 0)
            {
                if (control.FinishOrder(id))
                {
                    Console.WriteLine("\nOrdren " + id + " blev afsluttet");
                }
                else
                {
                    Console.WriteLine("\nOrdren " + id + " kunne ikke Afsluttes");
                }
            }
        }

        void ProductOrdered()
        {
            Console.Write("ID på det eksiterende produkt: ");
            string productIdTemp = Console.ReadLine();
            int.TryParse(productIdTemp, out int productId);

            Console.Write("Ordrenummer: ");
            string orderNumberTemp = Console.ReadLine();
            int.TryParse(orderNumberTemp, out int orderNumber);

            Console.Write("Ordre dato (DD-MM-YYYY): ");
            string date = Console.ReadLine();

            if (control.ProductOrdered(productId, orderNumber, date))
            {
                Console.WriteLine("Ordre med ordrenummer: " + orderNumber + " blev tilføjet...");
            }
            else
            {
                Console.WriteLine("Ordren kunne ikke oprettes...");
            }

            Console.ReadKey(true);
        }

        void RemoveProduct()
        {
            ShowAllProducts();
            Console.Write("\nVælg produkt ID på produktet som skal fjernes: ");
            string idTemp = Console.ReadLine();
            int.TryParse(idTemp, out int id);

            Console.Write("Er du sikker på at produktet med ID: " + id + " skal fjernes? (Y/N): ");
            string yesNo = Console.ReadLine().ToLower();

            if (yesNo == "y")
            {
                if (control.RemoveProduct(id))
                {
                    Console.WriteLine("Produktet med ID: " + id + " blev fjernet...");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Produktet kunne ikke fjernes...");
                    Console.ReadKey(true);
                }
            }
            else
            {
                Console.WriteLine("Produktet kunne ikke fjernes...");
                Console.ReadKey(true);
            }
        }

        void ShowAllProducts()
        {
            Console.Clear();
            Console.WriteLine("ID\t" + Utility.LengthenString("Navn") + "\tAntal\tBestilt\tPlacering");
            List<ProductType> products = control.GetAllProducts();

            foreach (ProductType p in products)
            {
                Console.WriteLine(p.ToString());
            } 
        }

        void UpdateNumberOfProducts()
        {
            Console.Write("Indtast ID på produktet: ");
            string idTemp = Console.ReadLine();
            int.TryParse(idTemp, out int id);

            Console.Write("Indtast det nye antal: ");
            string amountTemp = Console.ReadLine();
            int.TryParse(amountTemp, out int amount);

            if (control.UpdateNumberOfProducts(id, amount))
            {
                Console.WriteLine("Antallet blev opdateret til " + amount + "...");
            }
            else
            {
                Console.WriteLine("Antallet kunne ikke opdateres...");
            }
            Console.ReadKey(true);
        }

        void CreateProduct()
        {
            Console.Write("Produkt navn: ");
            string productName = Console.ReadLine();

            Console.Write("Antal: ");
            string amountTemp = Console.ReadLine();
            int.TryParse(amountTemp, out int amount);

            Console.Write("Placering: ");
            string placement = Console.ReadLine();

            try
            {
                control.CreateProduct(productName, amount, placement);
                Console.WriteLine("Produktet: " + productName + " blev tilføjet.");
            }
            catch (SqlConnectionException)
            {
                Console.WriteLine("Kunne ikke forbinde til databasen...");
                Console.WriteLine("Produktet kunne ikke tilføjes");
            }

            Console.ReadKey(true);
        }

        private void SearchProducts()
        {
            string searched = "Ingenting";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Viser resultat for: " + "'" + searched + "'");
                Console.WriteLine("ID\t" + Utility.LengthenString("Navn") + "\tAntal\tBestilt\tPlacering");

                if (searched == "0")
                {
                    break;
                }

                if (searched != "Ingenting" && searched != "")
                {
                    List<ProductType> searchedProducts = control.SearchProducts(searched);
                    if (searchedProducts.Count > 0)
                    {
                        foreach (ProductType p in searchedProducts)
                        {
                            Console.WriteLine(p.ToString());
                        }
                    }
                    else Console.WriteLine("Fandt ikke noget...");

                }
                
                Console.Write("\n\nIndtast starten af et ID eller en del af et navn (Tast '0' for afslut): ");
                searched = Console.ReadLine();
            }
        }
    }
}
