namespace FightingArena.Tests
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            this.arena = new Arena();
        }

        [Test]
        public void ConstructorShouldInitializeWarriorsCollection()
        {
            Arena ctorArena = new Arena();

            Assert.IsNotNull(ctorArena.Warriors);
        }

        [Test]
        public void EnrollingNonExistingWarriorShouldSuccess()
        {
            Warrior warrior = new Warrior("Pesho", 50, 100);

            this.arena.Enroll(warrior);

            bool isWarriorEnrolled = this.arena
                .Warriors
                .Contains(warrior);

            Assert.IsTrue(isWarriorEnrolled);
        }

        [Test]
        public void EnrollingSameWarriorShouldThrowException()
        {
            Warrior warrior = new Warrior("Pesho", 50, 100);

            this.arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(warrior);
            }, "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void EnrollingWarriorWithTheSameNameShouldThrowException()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Pesho", 45, 55);

            this.arena.Enroll(w1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(w2);
            }, "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void CountShouldReturnEnrolledWarriorsCount()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Gosho", 45, 100);

            this.arena.Enroll(w1);
            this.arena.Enroll(w2);

            int expectedCount = 2;
            int actualCount = this.arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void CountShouldReturnZeroWhenNoWarriorAreEnrolled()
        {
            int expectedCount = 0;
            int actualCount = this.arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void FightShouldThrowExceptionWithNonExistingAttacker()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Gosho", 30, 100);

            this.arena.Enroll(w2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(w1.Name, w2.Name);
            }, $"There is no fighter with name {w1.Name} enrolled for the fights!");
        }

        [Test]
        public void FightShouldThrowExceptionWithNonExistingDefender()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Gosho", 30, 100);

            this.arena.Enroll(w1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(w1.Name, w2.Name);
            }, $"There is no fighter with name {w2.Name} enrolled for the fights!");
        }

        [Test]
        public void FightShouldSucceedWhenWarriorsExist()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Gosho", 35, 100);

            int w1ExpectedHp = w1.HP - w2.Damage;
            int w2ExpectedHp = w2.HP - w1.Damage;

            this.arena.Enroll(w1);
            this.arena.Enroll(w2);

            this.arena.Fight(w1.Name, w2.Name);

            int w1ActualHp = this.arena.Warriors.First(w => w.Name == w1.Name).HP;
            int w2ActualHp = this.arena.Warriors.First(w => w.Name == w2.Name).HP;

            Assert.AreEqual(w1ExpectedHp, w1ActualHp);
            Assert.AreEqual(w2ExpectedHp, w2ActualHp);
        }

        //[Test]
        //public void WarriorShouldNotBeAbleToSelfAttack()
        //{
        //    Warrior w1 = new Warrior("Pesho", 50, 100);

        //    this.arena.Enroll(w1);

        //    Assert.Throws<InvalidOperationException>(() =>
        //    {
        //        this.arena.Fight(w1.Name, w1.Name);
        //    }, "You cannot attack yourself!");
        //}
    }
}
