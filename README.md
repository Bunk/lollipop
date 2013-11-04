Lollipop - A League of Legends Client API
========

![Lollipop](lollipop.png "Lollipop")

Have you ever wondered how sites like Elophant and LolKing get their data?  My guess (since I don't really know for sure) is that they use a technique similar to the one in this library.  Basically, it works like this:

- Pretend to be a League of Legends client by initiating a connection to the LoL server via RTMPS protocol
- Negotiate as a logged in user
- Send requests to the server for information and listen to events originating from LoL servers
- Profit

I'll write up some more documentation on the API here as time permits.
