# Messaging

Project completed during the fourth session of my software development program.

## Instructions

Using the language, framework, and database server of your choice, please design a secure web application. The chosen language should allow for the implementation of all the requirements in this statement.

Small constraint regarding the languages you can choose:

1. If you can host your website yourself (your own server, free online hosting, your home computer) and keep the server online and accessible publicly for a minimum of 2 weeks after submitting this assignment, 24/7, there are no restrictions on the language and type of database you can choose. In addition to your site's source files, you must also submit a text file named "url.txt" containing the address of your website.

2. If you do not want or cannot host your website online, it must be done with the following environment:
   (a) .Net Core/Framework Environment: To test and correct, I will start your project in Visual Studio by pressing the Play button, and everything should initialize correctly (including the database). It is your responsibility to ensure this is the case; please test it beforehand to be certain.

## Requirements

Your website should allow displaying a series of messages and posting new ones. It is a simplified version of a forum application that displays a succession of messages on a wall. You must implement the following functionalities at a minimum:

1. All necessary information and data for your site's proper functioning should be stored in a database.
2. Have a login form allowing the user to enter their username and password. Only users with a valid login can use your application.
3. Once logged in, the user sees a form allowing them to add a message to the wall.
4. Just below the add message form, there should be a list of messages, sorted by the date of addition in descending order (from the most recent to the oldest).
5. Each displayed message should show the full name of the user who sent the message, the creation date of the message, and the message content.
6. Users should be able to create messages on multiple lines and add line breaks (enter), which should be preserved when displayed in the list of messages. Messages can contain any characters, and the displayed content in the list of messages must be identical to what the user entered (including <, >, ", etc.).
7. Each message can receive comments, which will be displayed from the oldest to the most recent immediately below each message. For each comment, only display the full name of the user who made the comment and the comment content.
8. A maximum of 5 messages should be displayed per page, and it should be possible to change pages (with a button, link, or other) to show the next 5 messages (or previous ones when applicable) and so on to browse all messages.
9. Users can be of two types:
    (a) Normal User: Can send messages, reply to messages, and delete their own messages and replies.
    (b) Administrator: Can send messages, reply to messages, and delete any message and reply.

## Security Elements

Ensure correct management of the following security elements:
1. Database injections and uncontrolled data access (OWASP 2019: A3).
2. Secure session management for users (httponly, no sensitive information in cookies, reliable session token, etc., OWASP 2019: A7).
3. Access and privilege management (OWASP 2019: A1 + A2).
4. XSS (OWASP 2019: A3).
5. Secure password storage (salt, hash, ...).

Make sure to test your site with recent versions of Edge, Chrome, and Firefox.

While it would be essential if your web application were used in a real-world context, you do not have to implement security measures that strictly exceed the programming part of your website (certificates, https redirection, firewall, ...).
