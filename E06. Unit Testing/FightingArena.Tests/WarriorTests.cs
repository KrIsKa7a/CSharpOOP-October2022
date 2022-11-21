namespace FightingArena.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        //I assume that getter works just fine!
        [Test]
        public void ConstructorShouldInitializeWarriorName()
        {
            //Arrange
            string expectedName = "Pesho";
            Warrior warrior = new Warrior(expectedName, 50, 50);

            string actualname = warrior.Name;
            Assert.AreEqual(expectedName, actualname);
        }

        [Test]
        public void ConstructorShouldInitializeWarriorDamage()
        {
            int expectedDamage = 50;
            Warrior warrior = new Warrior("Pesho", expectedDamage, 100);

            int actualDamage = warrior.Damage;
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [Test]
        public void ConstructorShouldInitializeWarriorHP()
        {
            int expectedHp = 100;
            Warrior warrior = new Warrior("Pesho", 50, expectedHp);

            int actualHp = warrior.HP;
            Assert.AreEqual(expectedHp, actualHp);
        }

        //I assume that the constructor is tested!
        [TestCase("Pesho")]
        [TestCase("W")]
        [TestCase("Very very very very very very very very very very very long name")]
        public void NameSetterShouldSetValueWithValidName(string name)
        {
            Warrior warrior = new Warrior(name, 50, 100);

            string expectedName = name;
            string actualName = warrior.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("          ")]
        public void NameSetterShouldThrowExceptionWithEmptyOrWhiteSpaceName(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, 50, 100);
            }, "Name should not be empty or whitespace!");
        }

        [TestCase(50)]
        [TestCase(10000000)]
        [TestCase(1)]
        public void DamageSetterShouldSetValueWithValidDamage(int damage)
        {
            Warrior warrior = new Warrior("Pesho", damage, 100);

            int expectedDamage = damage;
            int actualDamage = warrior.Damage;

            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [TestCase(-50)]
        [TestCase(-1)]
        [TestCase(0)]
        public void DamageSetterShouldThrowExceptionWithZeroOrNegativeDamage(int damage)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Pesho", damage, 100);
            }, "Damage value should be positive!");
        }

        [TestCase(100)]
        [TestCase(50)]
        [TestCase(1)]
        [TestCase(0)]
        public void HPSetterShouldSetValueWithValidHP(int hp)
        {
            Warrior warrior = new Warrior("Pesho", 50, hp);

            int expectedHP = hp;
            int actualHP = warrior.HP;

            Assert.AreEqual(expectedHP, actualHP);
        }

        [TestCase(-50)]
        [TestCase(-1)]
        public void HPSetterShouldThrowExceptionWithNegativeHP(int hp)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Pesho", 50, hp);
            }, "HP should not be negative!");
        }

        [Test]
        public void SuccessAttackingWarriorNoKill()
        {
            //Arrange
            int w1Damage = 50;
            int w1Hp = 100;
            int w2Damage = 30;
            int w2Hp = 100;

            Warrior w1 = new Warrior("Pesho", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Gosho", w2Damage, w2Hp);

            //Act
            w1.Attack(w2);

            int w1ExpectedHp = w1Hp - w2Damage;
            int w2ExpectedHp = w2Hp - w1Damage;

            int w1ActualHp = w1.HP;
            int w2ActualHp = w2.HP;

            //Assert
            Assert.AreEqual(w1ExpectedHp, w1ActualHp);
            Assert.AreEqual(w2ExpectedHp, w2ActualHp);
        }

        [TestCase(35)]
        [TestCase(50)]
        public void SuccessAttackingWarriorWithKill(int w2Hp)
        {
            //Arrange
            int w1Damage = 50;
            int w1Hp = 100;
            int w2Damage = 30;

            Warrior w1 = new Warrior("Pesho", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Gosho", w2Damage, w2Hp);

            //Act
            w1.Attack(w2);

            int w1ExpectedHp = w1Hp - w2Damage;
            int w2ExpectedHp = 0;

            int w1ActualHp = w1.HP;
            int w2ActualHp = w2.HP;

            //Assert
            Assert.AreEqual(w1ExpectedHp, w1ActualHp);
            Assert.AreEqual(w2ExpectedHp, w2ActualHp);
        }

        [TestCase(0)]
        [TestCase(15)]
        [TestCase(30)]
        public void AttackingShouldThrowExceptionWhenAttackerHPIsBelowMin(int w1Hp)
        {
            //Arrange
            int w1Damage = 50;
            int w2Damage = 30;
            int w2Hp = 100;

            Warrior w1 = new Warrior("Pesho", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Gosho", w2Damage, w2Hp);

            //Act
            Assert.Throws<InvalidOperationException>(() =>
            {
                w1.Attack(w2);
            }, "Your HP is too low in order to attack other warriors!");
        }

        [TestCase(0)]
        [TestCase(15)]
        [TestCase(30)]
        public void AttackingShouldThrowExceptionWhenDefenderHPIsBelowMin(int w2Hp)
        {
            //Arrange
            int w1Damage = 50;
            int w1Hp = 100;
            int w2Damage = 30;

            Warrior w1 = new Warrior("Pesho", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Gosho", w2Damage, w2Hp);

            //Act
            Assert.Throws<InvalidOperationException>(() =>
            {
                w1.Attack(w2);
            }, "Enemy HP must be greater than 30 in order to attack him!");
        }

        [TestCase(45, 65)]
        [TestCase(45, 46)]
        public void AttackingShouldThrowExceptionWhenAttackerHPIsBelowDefenderDamage(int w1Hp, int w2Damage)
        {
            int w1Damage = 50;
            int w2Hp = 100;

            Warrior w1 = new Warrior("Pesho", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Gosho", w2Damage, w2Hp);

            //Act
            Assert.Throws<InvalidOperationException>(() =>
            {
                w1.Attack(w2);
            }, "You are trying to attack too strong enemy");
        }
    }
}