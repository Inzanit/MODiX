﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Rest
{
    /// <inheritdoc cref="RestTextChannel" />
    public interface IRestTextChannel : IRestGuildChannel, IIRestMessageChannel, ITextChannel
    {
        /// <inheritdoc cref="RestTextChannel.CreateWebhookAsync(string, Stream, RequestOptions)" />
        new Task<IRestWebhook> CreateWebhookAsync(string name, Stream avatar = null, RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetCategoryAsync(RequestOptions)" />
        Task<ICategoryChannel> GetCategoryAsync(RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetMessageAsync(ulong, RequestOptions)" />
        new Task<IRestMessage> GetMessageAsync(ulong id, RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetMessagesAsync(IMessage, Direction, int, RequestOptions)" />
        new IAsyncEnumerable<IReadOnlyCollection<IRestMessage>> GetMessagesAsync(IMessage fromMessage, Direction dir, int limit = 100, RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetMessagesAsync(ulong, Direction, int, RequestOptions)" />
        new IAsyncEnumerable<IReadOnlyCollection<IRestMessage>> GetMessagesAsync(ulong fromMessageId, Direction dir, int limit = 100, RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetMessagesAsync(int, RequestOptions)" />
        new IAsyncEnumerable<IReadOnlyCollection<IRestMessage>> GetMessagesAsync(int limit = 100, RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetPinnedMessagesAsync(RequestOptions)" />
        new Task<IReadOnlyCollection<IRestMessage>> GetPinnedMessagesAsync(RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetUserAsync(ulong, RequestOptions)" />
        Task<IRestGuildUser> GetUserAsync(ulong id, RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetUsersAsync(RequestOptions)" />
        IAsyncEnumerable<IReadOnlyCollection<IRestGuildUser>> GetUsersAsync(RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetWebhookAsync(ulong, RequestOptions)" />
        new Task<IRestWebhook> GetWebhookAsync(ulong id, RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.GetWebhooksAsync(RequestOptions)" />
        new Task<IReadOnlyCollection<IRestWebhook>> GetWebhooksAsync(RequestOptions options = null);

        /// <inheritdoc cref="RestTextChannel.SendFileAsync(string, string, bool, Embed, RequestOptions, bool, AllowedMentions)" />
        new Task<IRestUserMessage> SendFileAsync(string filePath, string text, bool isTTS = false, Embed embed = null, RequestOptions options = null, bool isSpoiler = false, AllowedMentions allowedMentions = null);

        /// <inheritdoc cref="RestTextChannel.SendFileAsync(Stream, string, string, bool, Embed, RequestOptions, bool, AllowedMentions)" />
        new Task<IRestUserMessage> SendFileAsync(Stream stream, string filename, string text, bool isTTS = false, Embed embed = null, RequestOptions options = null, bool isSpoiler = false, AllowedMentions allowedMentions = null);

        /// <inheritdoc cref="RestTextChannel.SendMessageAsync(string, bool, Embed, RequestOptions, AllowedMentions)" />
        new Task<IRestUserMessage> SendMessageAsync(string text = null, bool isTTS = false, Embed embed = null, RequestOptions options = null, AllowedMentions allowedMentions = null);
    }

    /// <summary>
    /// Provides an abstraction wrapper layer around a <see cref="Rest.RestTextChannel"/>, through the <see cref="IRestTextChannel"/> interface.
    /// </summary>
    internal class RestTextChannelAbstraction : RestGuildChannelAbstraction, IRestTextChannel
    {
        /// <summary>
        /// Constructs a new <see cref="RestTextChannelAbstraction"/> around an existing <see cref="Rest.RestTextChannel"/>.
        /// </summary>
        /// <param name="restTextChannel">The value to use for <see cref="Rest.RestTextChannel"/>.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="restTextChannel"/>.</exception>
        public RestTextChannelAbstraction(RestTextChannel restTextChannel)
            : base(restTextChannel) { }

        /// <inheritdoc />
        public ulong? CategoryId
            => RestTextChannel.CategoryId;

        /// <inheritdoc />
        public bool IsNsfw
            => RestTextChannel.IsNsfw;

        /// <inheritdoc />
        public string Mention
            => RestTextChannel.Mention;

        /// <inheritdoc />
        public int SlowModeInterval
            => RestTextChannel.SlowModeInterval;

        /// <inheritdoc />
        public string Topic
            => RestTextChannel.Topic;

        /// <inheritdoc />
        public async Task<IInviteMetadata> CreateInviteAsync(int? maxAge = 86400, int? maxUses = null, bool isTemporary = false, bool isUnique = false, RequestOptions options = null)
            => (await RestTextChannel.CreateInviteAsync(maxAge, maxUses, isTemporary, isUnique, options))
                .Abstract();

        /// <inheritdoc />
        public async Task<IRestWebhook> CreateWebhookAsync(string name, Stream avatar = null, RequestOptions options = null)
            => (await RestTextChannel.CreateWebhookAsync(name, avatar, options))
                .Abstract();

        /// <inheritdoc />
        async Task<IWebhook> ITextChannel.CreateWebhookAsync(string name, Stream avatar, RequestOptions options)
            => (await (RestTextChannel as ITextChannel).CreateWebhookAsync(name, avatar, options))
                .Abstract();

        /// <inheritdoc />
        public Task DeleteMessageAsync(ulong messageId, RequestOptions options = null)
            => RestTextChannel.DeleteMessageAsync(messageId, options);

        /// <inheritdoc />
        public Task DeleteMessageAsync(IMessage message, RequestOptions options = null)
            => RestTextChannel.DeleteMessageAsync(message, options);

        /// <inheritdoc />
        public Task DeleteMessagesAsync(IEnumerable<IMessage> messages, RequestOptions options = null)
            => RestTextChannel.DeleteMessagesAsync(messages, options);

        /// <inheritdoc />
        public Task DeleteMessagesAsync(IEnumerable<ulong> messageIds, RequestOptions options = null)
            => RestTextChannel.DeleteMessagesAsync(messageIds, options);

        /// <inheritdoc />
        public IDisposable EnterTypingState(RequestOptions options = null)
            => RestTextChannel.EnterTypingState(options);

        /// <inheritdoc />
        public async Task<ICategoryChannel> GetCategoryAsync(RequestOptions options = null)
            => (await RestTextChannel.GetCategoryAsync(options))
                .Abstract();

        /// <inheritdoc />
        public async Task<ICategoryChannel> GetCategoryAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (await (RestTextChannel as INestedChannel).GetCategoryAsync(mode, options))
                .Abstract();

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IInviteMetadata>> GetInvitesAsync(RequestOptions options = null)
            => (await RestTextChannel.GetInvitesAsync(options))
                .Select(InviteMetadataAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public async Task<IRestMessage> GetMessageAsync(ulong id, RequestOptions options = null)
            => (await RestTextChannel.GetMessageAsync(id, options))
                ?.Abstract();

        /// <inheritdoc />
        public async Task<IMessage> GetMessageAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (await (RestTextChannel as IMessageChannel).GetMessageAsync(id, mode, options))
                ?.Abstract();

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IRestMessage>> GetMessagesAsync(int limit = 100, RequestOptions options = null)
            => RestTextChannel.GetMessagesAsync(limit, options)
                .Select(x => x
                    .Select(RestMessageAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IMessage>> GetMessagesAsync(int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (RestTextChannel as IMessageChannel).GetMessagesAsync(limit, mode, options)
                .Select(x => x
                    .Select(MessageAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IRestMessage>> GetMessagesAsync(ulong fromMessageId, Direction dir, int limit = 100, RequestOptions options = null)
            => RestTextChannel.GetMessagesAsync(fromMessageId, dir, limit, options)
                .Select(x => x
                    .Select(RestMessageAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IMessage>> GetMessagesAsync(ulong fromMessageId, Direction dir, int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (RestTextChannel as IMessageChannel).GetMessagesAsync(fromMessageId, dir, limit, mode, options)
                .Select(x => x
                    .Select(MessageAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IRestMessage>> GetMessagesAsync(IMessage fromMessage, Direction dir, int limit = 100, RequestOptions options = null)
            => RestTextChannel.GetMessagesAsync(fromMessage, dir, limit, options)
                .Select(x => x
                    .Select(RestMessageAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IMessage>> GetMessagesAsync(IMessage fromMessage, Direction dir, int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (RestTextChannel as IMessageChannel).GetMessagesAsync(fromMessage, dir, limit, mode, options)
                .Select(x => x
                    .Select(MessageAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IRestMessage>> GetPinnedMessagesAsync(RequestOptions options = null)
            => (await RestTextChannel.GetPinnedMessagesAsync(options))
                .Select(RestMessageAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        async Task<IReadOnlyCollection<IMessage>> IMessageChannel.GetPinnedMessagesAsync(RequestOptions options)
            => (await (RestTextChannel as IMessageChannel).GetPinnedMessagesAsync(options))
                .Select(MessageAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public async Task<IRestGuildUser> GetUserAsync(ulong id, RequestOptions options = null)
            => (await RestTextChannel.GetUserAsync(id, options))
                ?.Abstract();

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IRestGuildUser>> GetUsersAsync(RequestOptions options = null)
            => RestTextChannel.GetUsersAsync(options)
                .Select(x => x
                    .Select(RestGuildUserAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public async Task<IRestWebhook> GetWebhookAsync(ulong id, RequestOptions options = null)
            => (await RestTextChannel.GetWebhookAsync(id, options))
                ?.Abstract();

        /// <inheritdoc />
        async Task<IWebhook> ITextChannel.GetWebhookAsync(ulong id, RequestOptions options)
            => (await (RestTextChannel as ITextChannel).GetWebhookAsync(id, options))
                ?.Abstract();

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IRestWebhook>> GetWebhooksAsync(RequestOptions options = null)
            => (await RestTextChannel.GetWebhooksAsync(options))
                .Select(RestWebhookAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        async Task<IReadOnlyCollection<IWebhook>> ITextChannel.GetWebhooksAsync(RequestOptions options)
            => (await (RestTextChannel as ITextChannel).GetWebhooksAsync(options))
                .Select(WebhookAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public Task ModifyAsync(Action<TextChannelProperties> func, RequestOptions options = null)
            => RestTextChannel.ModifyAsync(func, options);

        /// <inheritdoc />
        public async Task<IRestUserMessage> SendFileAsync(string filePath, string text, bool isTTS = false, Embed embed = null, RequestOptions options = null, bool isSpoiler = false, AllowedMentions allowedMentions = null)
            => (await RestTextChannel.SendFileAsync(filePath, text, isTTS, embed, options, isSpoiler, allowedMentions))
                .Abstract();

        /// <inheritdoc />
        async Task<IUserMessage> IMessageChannel.SendFileAsync(string filePath, string text, bool isTTS, Embed embed, RequestOptions options, bool isSpoiler, AllowedMentions allowedMentions)
            => (await (RestTextChannel as IMessageChannel).SendFileAsync(filePath, text, isTTS, embed, options, isSpoiler, allowedMentions))
                .Abstract();

        /// <inheritdoc />
        public async Task<IRestUserMessage> SendFileAsync(Stream stream, string filename, string text, bool isTTS = false, Embed embed = null, RequestOptions options = null, bool isSpoiler = false, AllowedMentions allowedMentions = null)
            => (await RestTextChannel.SendFileAsync(stream, filename, text, isTTS, embed, options, isSpoiler, allowedMentions))
                .Abstract();

        /// <inheritdoc />
        public async Task<IRestUserMessage> SendMessageAsync(string text = null, bool isTTS = false, Embed embed = null, RequestOptions options = null, AllowedMentions allowedMentions = null)
            => (await RestTextChannel.SendMessageAsync(text, isTTS, embed, options, allowedMentions))
                .Abstract();

        /// <inheritdoc />
        public Task SyncPermissionsAsync(RequestOptions options = null)
            => RestTextChannel.SyncPermissionsAsync(options);

        /// <inheritdoc />
        async Task<IUserMessage> IMessageChannel.SendFileAsync(Stream stream, string filename, string text, bool isTTS, Embed embed, RequestOptions options, bool isSpoiler, AllowedMentions allowedMentions)
            => (await (RestTextChannel as IMessageChannel).SendFileAsync(stream, filename, text, isTTS, embed, options, isSpoiler, allowedMentions))
                .Abstract();

        /// <inheritdoc />
        public Task TriggerTypingAsync(RequestOptions options = null)
            => (RestTextChannel as ITextChannel).TriggerTypingAsync(options);

        /// <inheritdoc />
        async Task<IUserMessage> IMessageChannel.SendMessageAsync(string text, bool isTTS, Embed embed, RequestOptions options, AllowedMentions allowedMentions)
            => (await (RestTextChannel as IMessageChannel).SendMessageAsync(text, isTTS, embed, options, allowedMentions))
                .Abstract();

        /// <summary>
        /// The existing <see cref="Rest.RestTextChannel"/> being abstracted.
        /// </summary>
        protected RestTextChannel RestTextChannel
            => RestGuildChannel as RestTextChannel;
    }

    /// <summary>
    /// Contains extension methods for abstracting <see cref="RestTextChannel"/> objects.
    /// </summary>
    internal static class RestTextChannelAbstractionExtensions
    {
        /// <summary>
        /// Converts an existing <see cref="RestTextChannel"/> to an abstracted <see cref="IRestTextChannel"/> value.
        /// </summary>
        /// <param name="restTextChannel">The existing <see cref="RestTextChannel"/> to be abstracted.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="restTextChannel"/>.</exception>
        /// <returns>An <see cref="IRestTextChannel"/> that abstracts <paramref name="restTextChannel"/>.</returns>
        public static IRestTextChannel Abstract(this RestTextChannel restTextChannel)
            => new RestTextChannelAbstraction(restTextChannel);
    }
}
