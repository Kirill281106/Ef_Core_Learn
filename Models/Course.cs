using System.ComponentModel.DataAnnotations;//если задать на строки чтото то к этому(MinLength,Key,Required,Range,EmailAddress,Phone)
                                            //-EmailAddress,Phone-проверка формата полей
using System.ComponentModel.DataAnnotations.Schema;//если менять названия чего либо то надо подкл к этому пространству(Table,Column,ForeignKey)

namespace EFCore.Models
{
    [Table("Courses")]//название табл
    internal class Course
    {
        [Key]//первичный ключ
        public int Id { get; set; }
        [Required]// следующее поле обязательно not null
        [MinLength(1)]//минимельная длина строки
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }// добавил для создания миграции
        public int LessonQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        [Column("Price")]// так присваивается название колонки
        public decimal Price { get; set; }
        [ForeignKey("AuthorId")]//внешний ключ или поле на которое ссылаемся
        public Author Author { get; set; }

    }
}
