namespace NavalVessels.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Contracts;

    public class VesselRepository : IRepository<IVessel>
    {
        private readonly ICollection<IVessel> vessels;

        public VesselRepository()
        {
            this.vessels = new HashSet<IVessel>();
        }

        public IReadOnlyCollection<IVessel> Models
            => (IReadOnlyCollection<IVessel>) this.vessels;

        public void Add(IVessel model)
        {
            this.vessels.Add(model);
        }

        public bool Remove(IVessel model)
            => this.vessels.Remove(model);

        public IVessel FindByName(string name)
            => this.vessels.FirstOrDefault(v => v.Name == name);
    }
}
