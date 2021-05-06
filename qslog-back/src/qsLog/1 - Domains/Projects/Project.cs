using System;
using qsLibPack.Domain.Entities;
using qsLibPack.Validations;

namespace qsLog.Domains.Projects
{
    public class Project : AggregateRoot<Guid>
    {
        public Project() {}
        public Project(string name)
        {
            Name = name;
            this.Id = Guid.NewGuid();
            this.GenerateApiKey();
            this.Validate();
        }

        public string Name { get; private set; }
        public Guid ApiKey { get; private set; }

        public void SetName(string value)
        {
            this.Name = value;
            this.Validate();
        }

        public void GenerateApiKey()
        {
            this.ApiKey = Guid.NewGuid();
        }

        protected override void Validate()
        {
            this.Id.NotNullOrEmpty();
            this.Name.NotNullOrEmpty();
            this.ApiKey.NotNullOrEmpty();
        }
    }
}