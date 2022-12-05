namespace SimpleSnake.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using GameObjects;
    using GameObjects.Foods;

    public class ReflectionHelper
    {
        public ICollection<Food> GenerateFoods(Wall wall)
        {
            ICollection<Food> foods = new List<Food>();

            Type[] foodTypes = this.GetFoodTypes();
            foreach (Type foodType in foodTypes)
            {
                Food instance = (Food)Activator
                    .CreateInstance(foodType, new object[] { wall });

                foods.Add(instance);
            }

            return foods;
        }

        private Type[] GetFoodTypes()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            Type[] foodTypes = assembly
                .GetTypes()
                .Where(t => t.Name.StartsWith("Food") && !t.IsAbstract)
                .ToArray();

            return foodTypes;
        }
    }
}
