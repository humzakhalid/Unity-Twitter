# Unity-Twitter

# Preview
https://youtu.be/uRSUdOfplck

# Supported unity: 
5.6 and above.

# Usage:
This example uses twitters services through its API calls, so I have created a small package which implements some of the API calls such as:

1) authenticating through twitter with a pin.
2) posting tweet on twitter.
3) searching a user on twitter.
4) sending direct message to a user.
5) searching a hashtag on twitter.

# Implentation Note:
To implement twitter in unity, create an app on https://apps.twitter.com/ , with filling out app name,description, website (any website) etc.
After app has been created get the consumer key and consumer key secret from the "Keys and access Tokens" section of your app.
Modify app permissions to "Read, Write and Access direct messages".
Place the consumer key and consumer key secret in DemoClass.cs placed on Demo Object in scene and you are good to go.

# Note:
All instructions to use package has been clearified in package in form of help texts.
As I have mentioned before, I have only implemented few main api calls, if further need to implement other calls arises, visit https://developer.twitter.com/en/docs/api-reference-index.html to get all api references and implement just like I have implemented api calls in DemoClass.cs and twitter.cs.

# Special Thanks To:
Nathalie and Justin from Microsoft who provided the updated code for Twitter API 1.1.
You can also check their github page for this project.
https://github.com/ProtossEngineering/TwitterAuth
