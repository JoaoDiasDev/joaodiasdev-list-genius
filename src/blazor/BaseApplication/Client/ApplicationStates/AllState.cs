namespace Client.ApplicationStates;

public class AllState
{
    //Scope action
    public Action? Action { get; set; }

    //General GeneralDepartment
    public bool ShowGeneralDepartment { get; set; }

    public void GeneralDepartmentClicked()
    {
        ResetAllStates();
        ShowGeneralDepartment = true;
        Action?.Invoke();
    }

    //GeneralDepartment
    public bool ShowDepartment { get; set; }

    public void DepartmentClicked()
    {
        ResetAllStates();
        ShowDepartment = true;
        Action?.Invoke();
    }

    //Branch
    public bool ShowBranch { get; set; }

    public void BranchClicked()
    {
        ResetAllStates();
        ShowBranch = true;
        Action?.Invoke();
    }

    //Country
    public bool ShowCountry { get; set; }

    public void CountryClicked()
    {
        ResetAllStates();
        ShowCountry = true;
        Action?.Invoke();
    }

    //City
    public bool ShowCity { get; set; }

    public void CityClicked()
    {
        ResetAllStates();
        ShowCity = true;
        Action?.Invoke();
    }

    //Town
    public bool ShowTown { get; set; }

    public void TownClicked()
    {
        ResetAllStates();
        ShowTown = true;
        Action?.Invoke();
    }

    //User
    public bool ShowUser { get; set; }

    public void UserClicked()
    {
        ResetAllStates();
        ShowUser = true;
        Action?.Invoke();
    }

    //Employee
    public bool ShowEmployee { get; set; } = true;

    public void EmployeeClicked()
    {
        ResetAllStates();
        ShowEmployee = true;
        Action?.Invoke();
    }

    private void ResetAllStates()
    {
        ShowGeneralDepartment = false;
        ShowDepartment = false;
        ShowBranch = false;
        ShowCountry = false;
        ShowCity = false;
        ShowTown = false;
        ShowUser = false;
        ShowEmployee = false;
        ShowHealth = false;
        ShowSanction = false;
        ShowSanctionType = false;
        ShowOvertime = false;
        ShowOvertimeType = false;
        ShowVacation = false;
        ShowVacationType = false;
        ShowUserProfile = false;
    }

    public bool ShowHealth { get; set; }
    public void HealthClicked()
    {
        ResetAllStates();
        ShowHealth = true;
        Action?.Invoke();
    }

    public bool ShowSanction { get; set; }

    public void SanctionClicked()
    {
        ResetAllStates();
        ShowSanction = true;
        Action?.Invoke();
    }
    public bool ShowSanctionType { get; set; }

    public void SanctionTypeClicked()
    {
        ResetAllStates();
        ShowSanctionType = true;
        Action?.Invoke();
    }
    public bool ShowOvertime { get; set; }

    public void OvertimeClicked()
    {
        ResetAllStates();
        ShowOvertime = true;
        Action?.Invoke();
    }

    public bool ShowOvertimeType { get; set; }

    public void OvertimeTypeClicked()
    {
        ResetAllStates();
        ShowOvertimeType = true;
        Action?.Invoke();
    }
    public bool ShowVacation { get; set; }

    public void VacationClicked()
    {
        ResetAllStates();
        ShowVacation = true;
        Action?.Invoke();
    }

    public bool ShowVacationType { get; set; }

    public void VacationTypeClicked()
    {
        ResetAllStates();
        ShowVacationType = true;
        Action?.Invoke();
    }

    public bool ShowUserProfile { get; set; }

    public void UserProfileClicked()
    {
        ResetAllStates();
        ShowUserProfile = true;
        Action?.Invoke();
    }

}