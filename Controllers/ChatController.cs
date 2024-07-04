using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat(string name, int creatorId)
    {
        var chat = await _chatService.CreateChatAsync(name, creatorId);
        return CreatedAtAction(nameof(GetChat), new { id = chat.Id }, chat);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchChats(string searchTerm)
    {
        var chats = await _chatService.SearchChatsAsync(searchTerm);
        return Ok(chats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetChat(int id)
    {
        var chat = await _chatService.GetChatByIdAsync(id);
        if (chat == null)
        {
            return NotFound();
        }
        return Ok(chat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChat(int id, int userId)
    {
        try
        {
            await _chatService.DeleteChatAsync(id, userId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{chatId}/messages")]
    public async Task<IActionResult> AddMessage(int chatId, int userId, string content)
    {
        var message = await _chatService.AddMessageAsync(chatId, userId, content);
        return CreatedAtAction(nameof(GetChat), new { id = chatId }, message);
    }
}