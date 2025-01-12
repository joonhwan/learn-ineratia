using System.Text.Json;
using AspApp.Models;

namespace AspApp.Services;


// Models/PaginationParameters.cs
public class PaginationParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 5;

    public int PageNumber { get; set; } = 1;
    
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    
    public static PaginationParameters All
    {
        get
        {
            var pp = new PaginationParameters
            {
                _pageSize = int.MaxValue
            };
            return pp;
        }
    }
}


public interface IJsonPlaceholderService
{
    Task<PaginatedResponse<Post>> GetPostsPaginatedAsync(PaginationParameters parameters);
    Task<PaginatedResponse<Comment>> GetCommentsPaginatedAsync(PaginationParameters parameters);
    Task<PaginatedResponse<User>> QueryUsers(PaginationParameters parameters, string? queryString = null);
    Task<PaginatedResponse<Todo>> GetTodosPaginatedAsync(PaginationParameters parameters);
    
    Task<List<Post>?> GetPostsAsync();
    Task<Post?> GetPostAsync(int id);
    Task<List<Comment>?> GetPostCommentsAsync(int postId);
    Task<Post?> CreatePostAsync(Post post);
    Task<Post?> UpdatePostAsync(int id, Post post);
    Task<bool> DeletePostAsync(int id);
    Task<List<Comment>?> GetCommentsAsync();
    Task<Comment?> GetCommentAsync(int id);
    Task<List<User>?> GetUsersAsync(string? queryString = null);
    Task<User?> GetUserAsync(int id);
    Task<List<Post>?> GetUserPostsAsync(int userId);
    Task<List<Todo>?> GetTodosAsync();
    Task<Todo?> GetTodoAsync(int id);
    Task<List<Todo>?> GetUserTodosAsync(int userId);
}

    
public class JsonPlaceholderService : IJsonPlaceholderService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<JsonPlaceholderService> _logger;

    public JsonPlaceholderService(HttpClient httpClient, ILogger<JsonPlaceholderService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    // 기존 메서드들은 그대로 유지하되, BaseUrl 상수는 제거
    // (HttpClient 설정에서 BaseAddress를 지정하므로)
    
    public async Task<PaginatedResponse<Post>> GetPostsPaginatedAsync(PaginationParameters parameters)
    {
        var allPosts = await GetPostsAsync();
        return CreatePaginatedResponse(allPosts, parameters);
    }

    public async Task<PaginatedResponse<Comment>> GetCommentsPaginatedAsync(PaginationParameters parameters)
    {
        var allComments = await GetCommentsAsync();
        return CreatePaginatedResponse(allComments, parameters);
    }

    public async Task<PaginatedResponse<User>> QueryUsers(PaginationParameters parameters, string? queryString = null)
    {
        var allUsers = await GetUsersAsync(queryString);
        return CreatePaginatedResponse(allUsers, parameters);
    }

    public async Task<PaginatedResponse<Todo>> GetTodosPaginatedAsync(PaginationParameters parameters)
    {
        var allTodos = await GetTodosAsync();
        return CreatePaginatedResponse(allTodos, parameters);
    }

    public Task<List<Post>?> GetPostsAsync()
    {
        return GetAsync<List<Post>>("/posts");
    }

    public Task<Post?> GetPostAsync(int id)
    {
        return GetAsync<Post>($"/posts/{id}");
    }

    public Task<List<Comment>?> GetPostCommentsAsync(int postId)
    {
        return GetAsync<List<Comment>>($"/posts/{postId}/comments");
    }

    public Task<Post?> CreatePostAsync(Post post)
    {
        return PostAsync<Post>("/posts", post);
    }

    public Task<Post?> UpdatePostAsync(int id, Post post)
    {
        return PutAsync<Post>($"/posts/{id}", post);
    }

    public Task<bool> DeletePostAsync(int id)
    {
        return DeleteAsync($"/posts/{id}");
    }

    public Task<List<Comment>?> GetCommentsAsync()
    {
        return GetAsync<List<Comment>>("/comments");
    }

    public Task<Comment?> GetCommentAsync(int id)
    {
        return GetAsync<Comment>($"/comments/{id}");
    }

    public Task<List<User>?> GetUsersAsync(string? queryString)
    {
        queryString = queryString?.Trim() ?? "";
        if (queryString.StartsWith("?") is false)
        {
            queryString = $"?{queryString}";
        }
        var url = $"/users{queryString}";
        _logger.LogInformation("GetUsersAsync : url : {Url}", url);
        return GetAsync<List<User>>(url);
    }

    public Task<User?> GetUserAsync(int id)
    {
        return GetAsync<User>($"/users/{id}");
    }

    public Task<List<Post>?> GetUserPostsAsync(int userId)
    {
        return GetAsync<List<Post>>($"/users/{userId}/posts");
    }

    public Task<List<Todo>?> GetTodosAsync()
    {
        return GetAsync<List<Todo>>("/todos");
    }

    public Task<Todo?> GetTodoAsync(int id)
    {
        return GetAsync<Todo>($"/todos/{id}");
    }

    public Task<List<Todo>?> GetUserTodosAsync(int userId)
    {
        return GetAsync<List<Todo>>($"/users/{userId}/todos");
    }
    
    
    private async Task<T?> GetAsync<T>(string url)
    {
        var response = await _httpClient.GetFromJsonAsync<T>(url);
        return response;
    }

    private async Task<T?> PostAsync<T>(string url, object data)
    {
        var response = await _httpClient.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task<T?> PutAsync<T>(string url, object data)
    {
        var response = await _httpClient.PutAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task<bool> DeleteAsync(string url)
    {
        var response = await _httpClient.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    
    private PaginatedResponse<T> CreatePaginatedResponse<T>(List<T>? items, PaginationParameters parameters)
    {
        items ??= new List<T>();
        
        var totalCount = items.Count;
        var pageItems = items
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToList();

        return new PaginatedResponse<T>
        {
            Items = pageItems,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalCount = totalCount
        };
    }
}