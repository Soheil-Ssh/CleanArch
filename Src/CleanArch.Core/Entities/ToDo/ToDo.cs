namespace CleanArch.Core.Entities.ToDo;

/// <summary>
/// Represents a to-do item with a title, optional description, completion status, and completion date.
/// Inherits common auditing and soft-delete properties from <see cref="BaseEntity{TKey}"/> with a <see cref="long"/> key.
/// </summary>
public class ToDo : BaseEntity<long>
{
    /// <summary>
    /// The title of the to-do item. This field is required and cannot exceed 150 characters.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public required string Title { get; set; }

    /// <summary>
    /// An optional longer description of the task. Maximum length is 1000 characters.
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the task is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// The date and time when the task was completed. <c>null</c> if the task has not been marked as complete.
    /// </summary>
    public DateTime? CompletedDate { get; set; }
}