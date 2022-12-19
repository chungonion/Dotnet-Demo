using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Staff
{
    [Key]
    public int StaffId { get; set; }
    
    public string StaffName { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public StaffRole StaffRole { get; set; }
    
    public int RoleId { get; set; }
}

public class StaffRole
{
    [Key]
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}