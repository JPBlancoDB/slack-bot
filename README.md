A chat bot sample for Slack with netcore.

## Configuration

1. Create the bot in [Slack](https://api.slack.com/apps?new_app=1)
2. Go to [OAuth & Permissions](https://api.slack.com/apps/APP_ID/oauth?)
3. Copy the _Bot User OAuth Access Token_ and paste it into the appsettings file:

```
  "SlackConfiguration": {
    "ApiUrl": "https://slack.com/api/",
    "BotToken": TOKEN
}
```
