using CleanArch.Domain.TodoListAggregate;
using CleanArch.Domain.TodoListAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infrastructure.Persistence.Configuration;

/// <summary>
/// Class which provides configuration for the EF Core DbContext concerning the <seealso cref="TodoList"/> Aggregate Entity. 
/// </summary>
public sealed class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        ConfigureTodoListTable(builder);
        ConfigureTodoItemTable(builder);
    }

    private void ConfigureTodoListTable(EntityTypeBuilder<TodoList> builder)
    {
        builder
            .HasKey(tl => tl.Id);

        builder
            .Property(tl => tl.Id)
            .HasConversion(
                id => id.Value,
                value => TodoListId.Create(value));

        builder.Property(tl => tl.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(tl => tl.Description)
            .IsRequired()
            .HasMaxLength(100);
    }

    /// <summary>
    /// Configures the settings for the table for the <see cref="TodoItem"/> Entity type.
    /// </summary>
    /// <param name="builder">API for configuring an EF Core Entity.</param>
    private void ConfigureTodoItemTable(EntityTypeBuilder<TodoList> builder)
    {
        builder
            .OwnsMany(tl => tl.Items, tib =>
            {
                tib.WithOwner().HasForeignKey("TodoListId");
                tib.HasKey("Id", "TodoListId");
                tib.Property(ti => ti.Id)
                    .HasConversion(
                        id => id.Value,
                        value => TodoItemId.Create(value));
                tib.Property(ti => ti.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                tib.Property(ti => ti.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        builder.Metadata
            .FindNavigation(nameof(TodoList.Items))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
