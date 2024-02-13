```
      • ▌ ▄ ·.  ▐ ▄ ▪  .▄▄ ·  ▄▄· ▪  ▄▄▄ . ▐ ▄ ▄▄▄▄▄
▪     ·██ ▐███▪•█▌▐███ ▐█ ▀. ▐█ ▌▪██ ▀▄.▀·•█▌▐█•██  
 ▄█▀▄ ▐█ ▌▐▌▐█·▐█▐▐▌▐█·▄▀▀▀█▄██ ▄▄▐█·▐▀▀▪▄▐█▐▐▌ ▐█.▪
▐█▌.▐▌██ ██▌▐█▌██▐█▌▐█▌▐█▄▪▐█▐███▌▐█▌▐█▄▄▌██▐█▌ ▐█▌·
 ▀█▄▀▪▀▀  █▪▀▀▀▀▀ █▪▀▀▀ ▀▀▀▀ ·▀▀▀ ▀▀▀ ▀▀▀ ▀▀ █▪ ▀▀▀ 
```
# Omniscient

Omniscient is a VERY minimalist Command and Control (C2) Command Line Interface (CLI). It provides a minimalistic interface for managing and controlling remote clients.

## Server

The server component acts as a TCP listener, waiting for connections from clients. It provides a range of commands to interact with and control the connected clients.

## Client

The client features:

- **KeyLogger**: Records the keys pressed on the client's machine.
- **WindowLogger**: Keeps track of the active window on the client's machine.
- **Command Executor**: Executes commands received from the server on the client's machine.

## Running the Application

To ensure the application functions correctly, it's important to start the server before initiating the client. Here are the steps to do so:

### Starting the Server

1. Open a terminal.
2. Navigate to the `server` directory from the root of the project:

    ```bash
    cd server
    ```

3. Once inside the `server` directory, start the server by running:

    ```bash
    dotnet run
    ```

    The server should now be running and waiting for client connections.

### Starting the Client

1. Open a new terminal.
2. Navigate to the `client` directory from the root of the project:

    ```bash
    cd client
    ```

3. Once inside the `client` directory, start the client by running:

    ```bash
    dotnet run
    ```

    The client should now be running and attempting to connect to the server.

## Commands

Here are the available commands in the Omniscient CLI:

- `help`: Displays a list of available commands and their descriptions.
- `list`: Lists all connected clients.
- `use <client_id>`: Connects to a specific client using its ID.
- `exec <command>`: Executes a command on the connected client.
- `close`: Closes the current connection to the client.
- `exit`: Shuts down the server.
- `get-log`: Retrieves the log file from the connected client.
- `sad`: Accesses additional sub-commands (type `sad help` for more info).

### Sub-Commands for `sad`

- `help`: Displays a list of `sad` sub-commands and their descriptions.
- `program`: Lists all programs installed on the client's machine.
- `vulne`: Lists potentially vulnerable programs on the client's machine (requires running the `program` command beforehand).
- `system`: Retrieves system information from the client's machine.


This project was made with [John](https://github.com/john-btg) and [Marius](https://github.com/mariuslupo)