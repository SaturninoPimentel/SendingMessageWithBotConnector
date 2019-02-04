using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;

namespace BotConnectorExample
{
    class Program
    {
        private static string botClientId = "bot_id";
        private static string botSecret = "bot_secret"; 
        //Tiende a ser constante, pero puede cambiar
        private static string serviceUrl = "service_url";
        private static string conversationId = "conversation_id";
        private static string recipientId = "recipient_Id";
        private static string fromId = "from_Id";

        static async Task Main(string[] args)
        {
            ConnectorClient connectorClient = GetConnectorClient();
            try
            {
                await SendMessageAsync(connectorClient,
                    conversationId,
                    recipientId,
                    fromId,
                    "Mensaje desde la consola.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static async Task SendMessageAsync(
            ConnectorClient connectorClient,
            string conversationId,
            string recipientId,
            string fromId,
            string message)
        {
            Activity messageActivity =
                new Activity();
            messageActivity.Type = ActivityTypes.Message;
            messageActivity.Text = message;
            messageActivity.ChannelId = Channels.Webchat;
            messageActivity.ServiceUrl = serviceUrl;
            messageActivity.Conversation = new ConversationAccount()
            {
                Id = conversationId
            };
            messageActivity.Recipient = new ChannelAccount()
            {
                Id = recipientId
            };
            messageActivity.From = new ChannelAccount()
            {
                Id = fromId
            };

            await connectorClient
                .Conversations
                .SendToConversationAsync(messageActivity);

        }

        static ConnectorClient GetConnectorClient()
        {

            MicrosoftAppCredentials appCredentials =
               new MicrosoftAppCredentials(botClientId, botSecret);
            MicrosoftAppCredentials.TrustServiceUrl(serviceUrl);

            Uri uri = new Uri(serviceUrl);
            ConnectorClient connectorClient =
                new ConnectorClient(uri, appCredentials);

            return connectorClient;
        }

    }
}
