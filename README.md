<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# Block Spam Calls. Powered by Twilio - C#/ASP.NET MVC
[![Build
Status](https://travis-ci.org/TwilioDevEd/block-spam-calls-csharp.svg?branch=master)](https://travis-ci.org/TwilioDevEd/block-spam-calls-csharp)

Learn how to use Twilio add-ons to block spam calls.

## Local development

First you need to [Visual Studio](https://www.visualstudio.com/downloads/).

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
Once you have started ngrok, update your TwiML app's voice URL setting to use your ngrok hostname, so it will look something like this:

```shell
http://88b37ada.ngrok.io/voice

```

That's it!

## Run the tests

You can run the tests in [Visual Studio.](https://msdn.microsoft.com/en-us/library/ms182470.aspx)

## Meta

* No warranty expressed or implied. Software is as is. Diggity.
* [MIT License](http://www.opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
