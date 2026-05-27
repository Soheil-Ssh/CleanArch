namespace CleanArch.Core.Entities.Base;

/// <summary>
/// Represents the base class for all entities, providing common properties for identity, auditing, and soft deletion.
/// </summary>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public abstract class BaseEntity<TKey>
{
    /// <summary>
    /// The primary key of the entity.
    /// </summary>
    [Key]
    public TKey Id { get; init; } = default!;

    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    public DateTime CreateDate { get; init; }

    /// <summary>
    /// The date and time of the last update, or <c>null</c> if the entity has never been modified.
    /// </summary>
    public DateTime? UpdateDate { get; init; }

    /// <summary>
    /// Indicates whether the entity is soft-deleted.
    /// </summary>
    public bool IsDeleted { get; init; }
}