//Engine will not always work with the console!
//We can have a ConsoleApp, WebApp, AndroidApp, iOSApp with the same Core logic
namespace Telephony.Core
{
    using System;

    using Interfaces;

    using Exceptions;
    using IO.Interfaces;
    using Models;
    using Models.Interfaces;

    public class Engine : IEngine
    {
        //I do not know the type of the reader and writer
        //Someone outside of the engine sets their type
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IStationaryPhone stationaryPhone;
        private readonly ISmartphone smartphone;

        private Engine()
        {
            this.stationaryPhone = new StationaryPhone();
            this.smartphone = new Smartphone();
        }

        public Engine(IReader reader, IWriter writer)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            //Using Console.ReadLine() method rejects another apps to use our logic..
            //We cannot have shared Engine class and shared business logic... :(
            string[] phoneNumbers = this.reader.ReadLine()
            .Split(' ');
            string[] urls = this.reader.ReadLine()
                .Split(' ');

            foreach (string phoneNumber in phoneNumbers)
            {
                try
                {
                    if (phoneNumber.Length == 10)
                    {
                        this.writer.WriteLine(this.smartphone.Call(phoneNumber));
                    }
                    else if (phoneNumber.Length == 7)
                    {
                        this.writer.WriteLine(this.stationaryPhone.Call(phoneNumber));
                    }
                    else
                    {
                        throw new InvalidPhoneNumberException();
                    }
                }
                catch (InvalidPhoneNumberException ipne)
                {
                    this.writer.WriteLine(ipne.Message);
                }
                catch(Exception)
                {
                    throw;
                }
            }

            foreach (string url in urls)
            {
                try
                {
                    this.writer.WriteLine(this.smartphone.BrowseURL(url));
                }
                catch (InvalidURLException iue)
                {
                    this.writer.WriteLine(iue.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
