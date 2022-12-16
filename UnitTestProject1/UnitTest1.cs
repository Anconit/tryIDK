using Microsoft.VisualStudio.TestTools.UnitTesting;
using PP2022;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CorrectAuthorizTry1()
        {
            MainWindow window = null;

            // The dispatcher thread
            var t = new Thread(() =>
            {
                window = new MainWindow();

                // Initiates the dispatcher thread shutdown when the window closes
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                window.Show();
                bool b = window.Authorizatiya("1", "1");
                Assert.AreEqual(true, b);
                // Makes the thread support message pumping
                System.Windows.Threading.Dispatcher.Run();
            });

            // Configure the thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [TestMethod]
        public void CorrectAuthorizTry2()
        {
            MainWindow window = null;

            // The dispatcher thread
            var t = new Thread(() =>
            {
                window = new MainWindow();

                // Initiates the dispatcher thread shutdown when the window closes
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                window.Show();
                bool b = window.Authorizatiya("2", "2");
                Assert.AreEqual(true, b);
                // Makes the thread support message pumping
                System.Windows.Threading.Dispatcher.Run();
            });

            // Configure the thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [TestMethod]
        public void CorrectAuthorizTry3()
        {
            MainWindow window = null;

            // The dispatcher thread
            var t = new Thread(() =>
            {
                window = new MainWindow();

                // Initiates the dispatcher thread shutdown when the window closes
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                window.Show();
                bool b = window.Authorizatiya("3", "3");
                Assert.AreEqual(true, b);
                // Makes the thread support message pumping
                System.Windows.Threading.Dispatcher.Run();
            });

            // Configure the thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [TestMethod]
        public void CorrectAuthorizTry4()
        {
            MainWindow window = null;

            // The dispatcher thread
            var t = new Thread(() =>
            {
                window = new MainWindow();

                // Initiates the dispatcher thread shutdown when the window closes
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                window.Show();
                bool b = window.Authorizatiya("4", "4");
                Assert.AreEqual(true, b);
                // Makes the thread support message pumping
                System.Windows.Threading.Dispatcher.Run();
            });

            // Configure the thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [TestMethod]
        public void CorrectAuthorizTry5()
        {
            MainWindow window = null;

            // The dispatcher thread
            var t = new Thread(() =>
            {
                window = new MainWindow();

                // Initiates the dispatcher thread shutdown when the window closes
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                window.Show();
                bool b = window.Authorizatiya("5", "5");
                Assert.AreEqual(true, b);
                // Makes the thread support message pumping
                System.Windows.Threading.Dispatcher.Run();
            });

            // Configure the thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [TestMethod]
        public void NotCorrectAuthorizTry6()
        {
            MainWindow window = null;

            // The dispatcher thread
            var t = new Thread(() =>
            {
                window = new MainWindow();

                // Initiates the dispatcher thread shutdown when the window closes
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                window.Show();
                bool b = window.Authorizatiya("f333asf", "asfafa2");
                Assert.AreEqual(false, b);
                // Makes the thread support message pumping
                System.Windows.Threading.Dispatcher.Run();
            });

            // Configure the thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [TestMethod]        
        public void CorrectZapisDeistiya()
        {
            bool a = ZapisDeistie.ZapisDeistiya(1, 1);
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void NotCorrectZapisDeistiya1()
        {
            bool a = ZapisDeistie.ZapisDeistiya(-1, 1);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void NotCorrectZapisDeistiya2()
        {
            bool a = ZapisDeistie.ZapisDeistiya(1, -1);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void NotCorrectZapisDeistiya3()
        {
            bool a = ZapisDeistie.ZapisDeistiya(16, 1);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void NotCorrectZapisDeistiya4()
        {
            bool a = ZapisDeistie.ZapisDeistiya(1, 19);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void CorrectDate()
        {
            DateTime date1 = new DateTime(2022, 11, 20, 18, 30, 25);
            bool a = Proverki.IsDateTimeNullorEmpty(date1);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void NotCorrectDate()
        {
            DateTime? date1 = null;
            bool a = Proverki.IsDateTimeNullorEmpty(date1);
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void ConnectionDataTovars()
        {
            var connect = PP2022Entities.GetContext().SkladTovars;
            bool prov = false;
            if (connect == null) prov = true;
            else prov = false;
            Assert.AreEqual(false, prov);
        }

        [TestMethod]
        public void ConnectionDataBraks()
        {
            var connect = PP2022Entities.GetContext().Braks;
            bool prov = false;
            if (connect == null) prov = true;
            else prov = false;
            Assert.AreEqual(false, prov);
        }

        [TestMethod]
        public void ConnectionDataZacazs()
        {
            var connect = PP2022Entities.GetContext().ZacazTovars;
            bool prov = false;
            if (connect == null) prov = true;
            else prov = false;
            Assert.AreEqual(false, prov);
        }

        [TestMethod]
        public void ConnectionDataRabotniks()
        {
            var connect = PP2022Entities.GetContext().Rabotnikis;
            bool prov = false;
            if (connect == null) prov = true;
            else prov = false;
            Assert.AreEqual(false, prov);
        }

        [TestMethod]
        public void ConnectionDataDeistviyas()
        {
            var connect = PP2022Entities.GetContext().Deistviyas;
            bool prov = false;
            if (connect == null) prov = true;
            else prov = false;
            Assert.AreEqual(false, prov);
        }
    }
}
