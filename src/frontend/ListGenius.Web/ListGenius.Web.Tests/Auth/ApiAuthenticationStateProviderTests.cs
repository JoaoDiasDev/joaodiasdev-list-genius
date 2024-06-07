using Blazored.LocalStorage;
using ListGenius.Web.Components.Auth;
using Moq;

public class ApiAuthenticationStateProviderTests
{
    [Fact]
    public async Task GetAuthenticationStateAsync_ReturnsAnonymous_WhenNoToken()
    {
        // Arrange
        var localStorageMock = new Mock<ILocalStorageService>();
        localStorageMock.Setup(x => x.GetItemAsync<string>("authToken", CancellationToken.None)).ReturnsAsync((string)null);
        var provider = new ApiAuthenticationStateProvider(localStorageMock.Object);

        // Act
        var state = await provider.GetAuthenticationStateAsync();

        // Assert
        Assert.False(state.User.Identity.IsAuthenticated);
    }

    [Fact]
    public async Task GetAuthenticationStateAsync_ReturnsAuthenticated_WhenTokenValid()
    {
        // Arrange
        var token = "your-valid-jwt-token";
        var expirationDate = DateTime.UtcNow.AddHours(1).ToString("o");
        var localStorageMock = new Mock<ILocalStorageService>();
        localStorageMock.Setup(x => x.GetItemAsync<string>("authToken", CancellationToken.None)).ReturnsAsync(token);
        localStorageMock.Setup(x => x.GetItemAsync<string>("tokenExpiration", CancellationToken.None)).ReturnsAsync(expirationDate);
        var provider = new ApiAuthenticationStateProvider(localStorageMock.Object);

        // Act
        var state = await provider.GetAuthenticationStateAsync();

        // Assert
        Assert.True(state.User.Identity.IsAuthenticated);
    }

    [Fact]
    public void MarkUserAsAuthenticated_SetsAuthenticatedUser()
    {
        // Arrange
        var localStorageMock = new Mock<ILocalStorageService>();
        var provider = new ApiAuthenticationStateProvider(localStorageMock.Object);
        var email = "test@example.com";

        // Act
        provider.MarkUserAsAuthenticated(email);
        var state = provider.GetAuthenticationStateAsync().Result;

        // Assert
        Assert.True(state.User.Identity.IsAuthenticated);
        Assert.Equal(email, state.User.Identity.Name);
    }

    [Fact]
    public void MarkUserAsLoggedOut_SetsAnonymousUser()
    {
        // Arrange
        var localStorageMock = new Mock<ILocalStorageService>();
        var provider = new ApiAuthenticationStateProvider(localStorageMock.Object);

        // Act
        provider.MarkUserAsLoggedOut();
        var state = provider.GetAuthenticationStateAsync().Result;

        // Assert
        Assert.False(state.User.Identity.IsAuthenticated);
    }
}
