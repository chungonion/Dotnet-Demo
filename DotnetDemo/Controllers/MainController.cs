using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("")]
public class MainController : ControllerBase
{
    private readonly IMainService _mainService;

    public MainController(IMainService mainService)
    {
        _mainService = mainService;
    }

    // GET
    [HttpGet]
    [Route("staffs/{staffId}")]
    public async Task<IActionResult> ReadStaffs([FromRoute]int staffId)
    {
        var staff = await _mainService.GetStaff(staffId);

        if (staff != null)
        {
            return Ok(staff);
        }

        return NotFound();
    }
    
    [HttpGet]
    [Route("staffs")]
    public async Task<IActionResult> ReadStaff([FromQuery]int? roleId = null)
    {
        var staffs = await _mainService.GetStaffs(roleId);

        if (staffs.Any())
        {
            return Ok(staffs);
        }

        return NotFound();
    }
    
    [HttpPost]
    [Route("staff")]
    public async Task<IActionResult> Create([FromBody] MainRequestBody requestBody)
    {
        var staff = new Staff
        {
            StaffName = requestBody.StaffName,
            RoleId = requestBody.RoleId,
        };

        await _mainService.CreateStaff(staff);
        return Ok(staff);
    }

    [HttpPut]
    [Route("staff")]
    public async Task<IActionResult> Update([FromBody] MainRequestBody requestBody)
    {
        var staff = await _mainService.GetStaff(requestBody.StaffId);
        if (staff == null)
        {
            return NotFound();
        }

        staff.StaffName = requestBody.StaffName;
        staff.RoleId = requestBody.RoleId;
        await _mainService.UpdateStaff(staff);
        return NoContent() ;
    }
    
    [HttpDelete]
    [Route("staff")]
    public async Task<IActionResult> Delete([FromBody] MainRequestBody requestBody)
    {
        var staff = await _mainService.GetStaff(requestBody.StaffId);
        if (staff == null)
        {
            return NotFound();
        }
        
        await _mainService.DeleteStaff(staff);
        return Ok("Deleted");
    }
}

public class MainRequestBody
{
    public int StaffId { get; set; }
    public string StaffName { get; set; } = "";
    public int RoleId { get; set; }
}