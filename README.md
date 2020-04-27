<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# Block Spam Calls. Powered by Twilio - C#/ASP.NET MVC

![](https://github.com/TwilioDevEd/block-spam-calls-csharp/workflows/NetFx/badge.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/github/TwilioDevEd/block-spam-calls-csharp?svg=true)](https://ci.appveyor.com/project/TwilioDevEd/block-spam-calls-csharp)

> We are currently in the process of updating this sample template. If you are encountering any issues with the sample, please open an issue at [github.com/twilio-labs/code-exchange/issues](https://github.com/twilio-labs/code-exchange/issues) and we'll try to help you.

Learn how to use Twilio add-ons to block spam calls.

Follow the beginning of the [Block Spam Calls and RoboCalls guide](https://www.twilio.com/docs/voice/tutorials/block-spam-calls-and-robocalls-csharp) to learn how to add the spam filtering add-ons.

## Local development

First you need [Visual Studio](https://www.visualstudio.com/downloads/).

To run the app locally:

1. Clone this repository and open the `BlockSpamCalls.sln`.

   ```shell
   git clone git@github.com:TwilioDevEd/block-spam-calls-csharp.git
   ```

1. Build the solution (`ctrl + shift + b`)

1. Run the application (hit `f5`).

1. To actually forward incoming calls, your development server will need to be publicly accessible. [We recommend using ngrok to solve this problem](https://www.twilio.com/blog/2015/09/6-awesome-reasons-to-use-ngrok-when-testing-webhooks.html)
   
   ```shell
   $ choco install ngrok.portable
   ```

   1. Run ngrok:
      
      ```shell
      ngrok http 8080 --host-header="localhost:8080"
      ```

   1. Or, you can install [Ngrok Extensions](https://marketplace.visualstudio.com/items?itemName=DavidProthero.NgrokExtensions) for Visual Studio.

1. Once you have started ngrok, update your TwiML app's voice URL setting to use your ngrok hostname, so it will look something like this:

   ```shell
   http://88b37ada.ngrok.io/voice
   ```

That's it!

## Run the tests

You can run the tests in [Visual Studio.](https://msdn.microsoft.com/en-us/library/ms182470.aspx)

## Meta

* No warranty expressed or implied. Software is as is. Diggity.
* The CodeExchange repository can be found [here](https://github.com/twilio-labs/code-exchange/).
* [MIT License](http://www.opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
