using BaseLibrary.DTOs;

namespace Client.ApplicationStates;
public class UserProfileState
{
    public Action? Action { get; set; }
    public UserProfileDto UserProfile { get; set; } = new();
    public void ProfileUpdated() => Action!.Invoke();
}
