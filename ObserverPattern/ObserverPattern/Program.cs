﻿using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    /// <summary>
    /// The Subject abstract class
    /// </summary>
    abstract class Veggies
    {
        private double _pricePerPound;
        private List<IRestaurant> _restaurants = new List<IRestaurant>();

        public Veggies(double pricePerPound)
        {
            _pricePerPound = pricePerPound;
        }

        public void Attach(IRestaurant restaurant)
        {
            _restaurants.Add(restaurant);
        }

        public void Detach(IRestaurant restaurant)
        {
            _restaurants.Remove(restaurant);
        }

        public void Notify()
        {
            foreach (IRestaurant restaurant in _restaurants)
            {
                restaurant.Update(this);
            }

            Console.WriteLine("");
        }

        public double PricePerPound
        {
            get { return _pricePerPound; }
            set
            {
                if (_pricePerPound != value)
                {
                    _pricePerPound = value;
                    Notify(); //Automatically notify our observers of price changes
                }
            }
        }
    }

    /// <summary>
    /// The ConcreteSubject class
    /// </summary>
    class Carrots : Veggies
    {
        public Carrots(double price) : base(price) { }
    }

    /// <summary>
    /// The Observer interface
    /// </summary>
    interface IRestaurant
    {
        void Update(Veggies veggies);
    }

    /// <summary>
    /// The ConcreteObserver class
    /// </summary>
    class Restaurant : IRestaurant
    {
        private readonly string _name;
        private Veggies _veggie;
        private double _purchaseThreshold;

        public Restaurant(string name, double purchaseThreshold)
        {
            _name = name;
            _purchaseThreshold = purchaseThreshold;
        }

        public void Update(Veggies veggie)
        {
            Console.WriteLine("Notified {0} of {1}'s " + " price change to {2:C} per pound.", _name, veggie.GetType().Name, veggie.PricePerPound);
            if (veggie.PricePerPound < _purchaseThreshold)
            {
                Console.WriteLine(_name + " wants to buy some " + veggie.GetType().Name + "!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create price watch for Carrots and attach restaurants that buy carrots from suppliers.
            Carrots carrots = new Carrots(0.82);
            carrots.Attach(new Restaurant("Mackay's", 0.77));
            carrots.Attach(new Restaurant("Johnny's Sports Bar", 0.74));
            carrots.Attach(new Restaurant("Salad Kingdom", 0.75));

            // Fluctuating carrot prices will notify subscribing restaurants.
            carrots.PricePerPound = 0.79;
            carrots.PricePerPound = 0.76;
            carrots.PricePerPound = 0.74;
            carrots.PricePerPound = 0.81;

            Console.ReadKey();
        }
    }
}