namespace MilitaryElite.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;
    using IO.Interfaces;
    using Models;
    using Models.Enums;
    using Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly ICollection<ISoldier> allSoldiers;

        private Engine()
        {
            this.allSoldiers = new HashSet<ISoldier>();
        }

        public Engine(IReader reader, IWriter writer)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            this.CreateSoldiers();
            this.PrintSoldiers();
        }

        private void CreateSoldiers()
        {
            string command;
            while ((command = this.reader.ReadLine()) != "End")
            {
                string[] cmdArgs = command
                    .Split(' ');

                string soldierType = cmdArgs[0];
                int id = int.Parse(cmdArgs[1]);
                string firstName = cmdArgs[2];
                string lastName = cmdArgs[3];

                ISoldier soldier;
                if (soldierType == "Private")
                {
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    soldier = new Private(id, firstName, lastName, salary);
                }
                else if (soldierType == "LieutenantGeneral")
                {
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    ICollection<IPrivate> privates = this.FindPrivates(cmdArgs);

                    soldier = new LieutenantGeneral(id, firstName, lastName, salary, privates);
                }
                else if (soldierType == "Engineer")
                {
                    decimal salary = decimal.Parse(cmdArgs[4]);

                    string corpsText = cmdArgs[5];
                    bool isCorpsValid = Enum.TryParse<Corps>(corpsText, false, out Corps corps);
                    if (!isCorpsValid)
                    {
                        continue;
                    }

                    ICollection<IRepair> repairs = this.CreateRepairs(cmdArgs);
                    soldier = new Engineer(id, firstName, lastName, salary, corps, repairs);
                }
                else if (soldierType == "Commando")
                {
                    decimal salary = decimal.Parse(cmdArgs[4]);

                    string corpsText = cmdArgs[5];
                    bool isCorpsValid = Enum.TryParse<Corps>(corpsText, true, out Corps corps);
                    if (!isCorpsValid)
                    {
                        continue;
                    }

                    ICollection<IMission> missions = this.CreateMissions(cmdArgs);
                    soldier = new Commando(id, firstName, lastName, salary, corps, missions);
                }
                else if (soldierType == "Spy")
                {
                    int codeNumber = int.Parse(cmdArgs[4]);

                    soldier = new Spy(id, firstName, lastName, codeNumber);
                }
                else
                {
                    continue;
                }

                this.allSoldiers.Add(soldier);
            }
        }

        private ICollection<IPrivate> FindPrivates(string[] cmdArgs)
        {
            int[] privatesIds = cmdArgs
                .Skip(5)
                .Select(int.Parse)
                .ToArray();

            ICollection<IPrivate> privates = new HashSet<IPrivate>();
            foreach (int privateId in privatesIds)
            {
                IPrivate currPrivate = (IPrivate)this.allSoldiers
                    .FirstOrDefault(s => s.Id == privateId);

                privates.Add(currPrivate);
            }

            return privates;
        }

        private ICollection<IRepair> CreateRepairs(string[] cmdArgs)
        {
            ICollection<IRepair> repairs = new HashSet<IRepair>();

            string[] repairsInfo = cmdArgs
                .Skip(6)
                .ToArray();
            for (int i = 0; i < repairsInfo.Length; i += 2)
            {
                string partName = repairsInfo[i];
                int hoursWorked = int.Parse(repairsInfo[i + 1]);

                IRepair repair = new Repair(partName, hoursWorked);
                repairs.Add(repair);
            }

            return repairs;
        }

        private ICollection<IMission> CreateMissions(string[] cmdArgs)
        {
            ICollection<IMission> missions = new HashSet<IMission>();

            string[] missionsInfo = cmdArgs
                .Skip(6)
                .ToArray();
            for (int i = 0; i < missionsInfo.Length; i += 2)
            {
                string codeName = missionsInfo[i];

                string stateText = missionsInfo[i + 1];
                bool isStateValid = Enum.TryParse<State>(stateText, false, out State state);
                if (!isStateValid)
                {
                    continue;
                }

                IMission mission = new Mission(codeName, state);
                missions.Add(mission);
            }

            return missions;
        }

        private void PrintSoldiers()
        {
            foreach (ISoldier soldier in this.allSoldiers)
            {
                this.writer.WriteLine(soldier.ToString());
            }
        }
    }
}
