# REST Input Service

Allows you to send key input to a Windows machine using regular REST calls.

# Depedencies

To build this you'll need the following:

Needs
* Visual Studio Community 2015
* .NET 4.5 or later
* GrapeVine (https://github.com/sukona/Grapevine)
* Newtonsoft.Json 8.0.3+ (NuGet Package)
* InputSimulator 1.0.4+ (NuGet Package)

Please place the GrapeVine package in the parent folder of this project and make sure to name it "Grapevine" or the solution file won't find it.

# Installation

Run the provided setup, once installed, please execute the following as Admin in Windows:

`netsh http add urlacl url=http://+:5000/ user=everyone listen=yes`

Without this change, you won't be able to connect to this REST service from the outside of your computer, defeating the purpose of it.

# Usage

There are three endpoints:

## GET /actions

Returns a JSON array of various key-input actions you can send

## POST /interact

By posting a JSON to this endpoint, you'll be able to influence the computer. The format of the JSON is as follows:

```
{
  "action": [ "<KEY>", "<KEY>", ... ],
  "text" : "..."
}
```

The action field, if present, contains one or more text strings which can be found in the previous end-point. By prefixing the string with + it means to hold down that key, and - means to release that key. No prefix means that it's pressed and released. So to send a letter A in uppercase, you'd send:

```
{
  "action": [ "+LSHIFT", "VK_A", "-LSHIFT" ]
}
```

The text field is raw text input, anything you'd like.

If BOTH fields are present, action is executed first followed by text.

On success, you get text/plain "OK" and a 200 return code, on failure you get a stack-trace or other explaining information.

## POST /power

You can place the computer running this service in hibernation or suspend by posting the following (in JSON format):

```
{
  "state": "<suspend|hibernate>"
}
```

This of course means that you won't be able to communicate anymore with the computer and the last thing you'll get is an "OK" response on success. It's recommended you have it set up in such a way that issuing a Wake-on-Lan packet powers it back on again, or this will be a very annoying experience.

# UI

When you start this application, it will place an icon in your notification area (systray) and the only option exposed to terminate the service.
