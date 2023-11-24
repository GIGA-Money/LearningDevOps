# Traffic Simulation Project Documentation

**Introduction**

The Traffic Simulation Project is designed to model and analyze traffic flow through a network of signals. It features dynamic vehicle routing based on signal cycles and real-time congestion, offering insights into traffic management and optimization.

**Project Structure** 

The project consists of three main components:

1. **Signal.py**: Represents individual traffic signals and manages vehicle queues.

2. **Vehicle.py**: Represents vehicles navigating through the network.

3. **Simulation.py**: Orchestrates the entire simulation, creating the network and processing vehicle movements.

### **Signal.py**

* **Class Signal**: Manages a single traffic signal.  

    * **Attributes**:

        * `name`: Unique identifier for the signal.

        * `cycle_time`: Duration of a complete traffic light cycle. 

        * `outbound_nodes`: Outbound connections to other signals.

        * `queues`: Queues of vehicles for each outbound direction.

    * **Key Methods**:
                    
        * `process_queue`: Processes vehicle queues based on the signal's light schedule.

        * `add_vehicle_to_queue`: Adds a vehicle to the appropriate queue.

        * `find_route_dfs`: Finds a route using Depth-First Search.

        * `find_route_bfs`: Finds a route using Breadth-First Search.

### **Vehicle.py**  

* **Class Vehicle**: Represents a vehicle within the simulation.

    * **Attributes**:
       
        * `identifier`: Unique identifier for the vehicle.
   
        * `route`: List of signals representing the vehicle's route.
   
        * `start_time`: The time when the vehicle enters the network.

    * **Key Methods**:

        * `move`: Moves the vehicle along its route.

        * `reroute`: Reroutes the vehicle in case of congestion.

        * `update_route`: Updates the vehicle's route.

### **Simulation.py**  

* **Functionality**: Coordinates the entire simulation process.

    * **Key Operations**:

        * Creating a network of signals.

        * Generating vehicles and assigning routes. 

        * Simulating vehicle movements and handling rerouting.
                    
    * **Metrics Analysis**: Calculates and displays metrics such as average delay per vehicle and total vehicles rerouted.

**Usage Guide**

1. **Setup**:

    * Install Python 3.x.

    * Clone the repository and navigate to the project directory.
        
2. **Running the Simulation**:

    * Execute `python simulation.py` to start the simulation.
        
3. **Customization**:

    * Modify `simulation.py` to create custom traffic networks and scenarios.
        
4. **Testing**:

    * Run `python -m unittest test_simulation.py` to execute unit tests.

**Testing**   

The project includes unit tests to validate the functionality of individual components and their integration. Test cases cover a variety of scenarios, including edge cases.