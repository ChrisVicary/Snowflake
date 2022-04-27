# Event System

## A Couple of Changes

For the event system I have decided to stay very close to TheChero's implementation, just translating into C# with a couple of adjustments. 

The use of macros to generate the boiler plate code for various Get methods isn't something we can easily do in C#. For now I have just typed them all out, I could use something like a Source Generator to create the code, but it seemed unnecessary for this small amount of files. If we add more events, or allow client code to create their own events I might revisit this decision.

The EventType enum is the other interesting one, I would argue that this isn't needed in C# as we have language features to test for types. Seeing as all the event classes are derived from the Event class, the type information is already available to us. The EventDispatcher class uses the `GetStaticEventType()` method to get the type of events to dispatch and only call the function if the current event matches that type. Because of the difference between C++ templating and C# generics as well as the macros ability to generate the static method this just doesn't work well in C#. I have changed the EventDispatcher to just check the type of the event using the `is` operator rather than using the enum. This change arguably seems to make the EventType enum pointless, so I have removed it for now.

## Unit Testing

These changes see the first real pieces of the engine in place. One thing that I was a little disappointed about in the video series was that there seems to be no interest in testing. I supposed I can understand that the purpose of the series is to show a game engine, not a deep dive on testing. I truely believe in having a strong test suite to backup your code, as the codebase grows if you maintain good code coverage you can be confident that any changes you make aren't breaking existing functionality.

I'm not going to go into any great detail here either, but I have created a test assembly and filled out tests for all of the event system classes. I've chosen NUnit for my test framework, this is just the framework that I'm most familiar with. I've also added a dependency on Moq the excellent .Net mocking library.

## Video Link

[TheCherno - Game Engine Series - EventSystem](https://www.youtube.com/watch?v=sULV3aB2qeU&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=9&ab_channel=TheCherno)

## Next
[Window Abstraction](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/07-WindowAbstraction.md)