using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostDocuments.Models
{
    public class Documents
    {
        [Key]
        public int DocumentId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nome do Cliente")]
        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string ClientName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Status")]
        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Status { get; set; }
        public int StatusId { get; set; }

        [DisplayName("Arquivo")]
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Documents> ListStatus()
        {
            return new List<Documents>
            {
                new Documents { StatusId = 1, Status = "Aprovado"},
                new Documents { StatusId = 2, Status = "Pendente"},
                new Documents { StatusId = 3, Status = "Reprovado" }
            };
        }


        [DisplayFormat(DataFormatString ="{0:MMM-dd-yy}")]
        [DisplayName("Data")]
        public DateTime Date { get; set; }
    }

}
