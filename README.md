# Coffee Shop Chatbot — TCP Socket Programming

> Software Construction Lab | Semester 5 | Fall 2025

## Project Overview

A client-server chatbot application that simulates a coffee shop ordering system using TCP socket programming. The server acts as a coffee shop bot that displays a menu and processes customer orders, while the client provides a Windows Forms GUI for users to interact with the server in real-time.

## Academic Context

This project was developed as part of the Software Construction Lab course during the fifth semester. The primary objective was to understand and implement TCP/IP socket programming concepts, including client-server architecture, network streams, and multi-threaded communication.

## Problem Statement

Coffee shops need efficient ordering systems that can handle customer requests in real-time. Traditional ordering methods can be slow and prone to errors, especially during peak hours.

This project addresses the need for:
- A real-time communication system between customer and server
- An automated chatbot that can process orders instantly
- A user-friendly interface for placing orders
- A foundation for understanding network programming concepts

## Features Implemented

### Server Features
- **TCP Listener**: Listens for incoming client connections on port 9000
- **Menu System**: Displays a coffee menu with prices (Espresso, Cappuccino, Latte)
- **Order Processing**: Accepts orders by number or coffee name
- **Real-time Responses**: Sends immediate confirmation for each order
- **Error Handling**: Gracefully handles client disconnections and errors
- **Multi-client Support**: Can handle multiple clients sequentially

### Client Features
- **Windows Forms GUI**: User-friendly chat interface
- **Real-time Communication**: Instant message exchange with server
- **Background Listening**: Non-blocking thread for receiving server messages
- **Enter Key Support**: Send messages by pressing Enter
- **Connection Status**: Displays connection state and errors
- **Clean Disconnection**: Properly closes resources when form is closed

### Chatbot Functionality
- **Menu Display**: Shows available coffee options with prices
- **Order by Number**: Select coffee using numbers (1-3)
- **Order by Name**: Type coffee name directly (e.g., "espresso")
- **Menu Reload**: Type "menu" to see options again
- **Exit Command**: Type "exit" to disconnect gracefully

## Technology Stack

| Component | Technology |
|-----------|------------|
| Language | C# (.NET) |
| Server | Console Application |
| Client | Windows Forms |
| Protocol | TCP/IP |
| IDE | Visual Studio 2022 |
| Network | Socket Programming |

## Project Structure

```
socket-programming-chatbot/
├── README.md                    # This file
├── .gitignore                   # Git ignore rules
├── CoffeeServer/                # Server-side application
│   ├── Program.cs               # Server entry point and logic
│   ├── CoffeeServer.csproj      # Server project file
│   └── CoffeeServer.sln         # Server solution file
└── CoffeeClient/                # Client-side application
    ├── Form1.cs                 # Client form logic
    ├── Form1.Designer.cs        # Client form layout
    ├── Form1.resx               # Client form resources
    ├── Program.cs               # Client entry point
    ├── CoffeeClient.csproj      # Client project file
    └── CoffeeClient.sln         # Client solution file
```

## How to Run

### Prerequisites
- Windows OS
- .NET Framework or .NET 6.0+
- Visual Studio 2022 (recommended)

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/Khizar525/socket-programming-chatbot.git
   cd socket-programming-chatbot
   ```

2. **Start the Server First**
   - Open `CoffeeServer/CoffeeServer.sln` in Visual Studio
   - Build and run the project (F5)
   - The server console will display "Waiting for customers..."

3. **Start the Client**
   - Open `CoffeeClient/CoffeeClient.sln` in Visual Studio
   - Build and run the project (F5)
   - The client will connect to the server automatically

4. **Place an Order**
   - The menu will appear in the chat window
   - Type a number (1-3) or coffee name
   - Press Enter or click SEND
   - View the server's response

### Available Commands
| Command | Description |
|---------|-------------|
| `1` or `espresso` | Order Espresso (Rs 300) |
| `2` or `cappuccino` | Order Cappuccino (Rs 350) |
| `3` or `latte` | Order Latte (Rs 400) |
| `menu` | Display the menu again |
| `exit` | Disconnect from server |

## Architecture

```
┌─────────────────┐         TCP/IP          ┌─────────────────┐
│   CoffeeClient  │ ◄──────────────────────► │   CoffeeServer  │
│  (Windows Forms) │      Port 9000          │   (Console)     │
│                 │                          │                 │
│  - Chat GUI     │                          │  - Menu System  │
│  - Input Field  │                          │  - Order Logic  │
│  - Send Button  │                          │  - Bot Response │
└─────────────────┘                          └─────────────────┘
```

## Key Concepts Learned

### Network Programming
- **TCP/IP Protocol**: Understanding reliable transmission control protocol
- **Socket Programming**: Creating endpoints for network communication
- **Client-Server Model**: Implementing the fundamental network architecture
- **Network Streams**: Reading and writing data over network connections

### C# Programming
- **TcpListener/TcpServer**: Using .NET classes for TCP communication
- **StreamReader/StreamWriter**: Handling text-based data transmission
- **Multi-threading**: Running background tasks without blocking the UI
- **Windows Forms**: Building graphical user interfaces in C#

### Software Design
- **Separation of Concerns**: Distinct server and client responsibilities
- **Error Handling**: Graceful handling of network errors and disconnections
- **Resource Management**: Proper cleanup of network resources
- **Event-driven Programming**: Handling user interactions and network events

### Real-world Applications
- **Chat Applications**: Understanding how messaging apps work
- **Online Ordering Systems**: Foundation for e-commerce platforms
- **Real-time Communication**: Concepts used in WhatsApp, Uber, OLA

## Future Improvements

### Short-term Enhancements
- [ ] Add more coffee items to the menu
- [ ] Implement order confirmation with "yes/no"
- [ ] Add a simple billing system
- [ ] Support multiple simultaneous clients using threads

### Medium-term Features
- [ ] Add user authentication (login system)
- [ ] Implement order history
- [ ] Add a database for persistent storage
- [ ] Create an admin panel for menu management

### Long-term Vision
- [ ] Implement UDP for faster communication
- [ ] Add encryption for secure communication
- [ ] Create a web-based client using SignalR
- [ ] Deploy on a cloud server for remote access

## Course Information

**Course**: Software Construction Lab  
**Semester**: 5  
**Institution**: Bahria University Karachi  
**Academic Year**: Fall-2025

## Author

**M. Khizar Akram**  
BSE-5(B)  
Enrollment: 02-131232-064

## License

This project was developed for academic purposes as part of the Software Construction Lab course. It is intended for educational use and learning demonstration.

---

**Note**: This project demonstrates TCP socket programming concepts and is designed to showcase practical application of network programming. It represents a lab task for understanding client-server architecture.
