﻿namespace TransactionsSystem.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}