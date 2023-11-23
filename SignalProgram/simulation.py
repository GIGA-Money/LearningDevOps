from vehicle import Vehicle
from Signal import Signal
import argparse
import json

signal_network = {}


def create_traffic_network(config):
    signals = {}
    for signal_name, signal_info in config.items():
        signal = Signal(
            signal_name,
            signal_info['cycle_time'],
            signal_info['outbound_nodes'],
            signal_info['queue_sizes']
        )
        signal.update_light_schedule(signal_info['light_schedule'])
        signals[signal_name] = signal
    return signals


def create_traffic_network():
    # Create signals and define their connections and light cycles
    # Example:
    signals = {
        'A': Signal('A', 60, {'B': 'N', 'C': 'E'}, {'N': 10, 'E': 10, 'S': 10, 'W': 10}),
        'B': Signal('B', 60, {'A': 'S', 'C': 'E'}, {'N': 10, 'E': 10, 'S': 10, 'W': 10}),
        # More signals as needed
    }
    # Define light schedules for each signal
    # Example:
    signals['A'].update_light_schedule(
        {'N': (0, 15), 'E': (15, 30), 'S': (30, 45), 'W': (45, 60)})
    signals['B'].update_light_schedule(
        {'N': (0, 15), 'E': (15, 30), 'S': (30, 45), 'W': (45, 60)})
    # More schedules as needed
    return signals


def simulate_traffic(signals, total_simulation_time, vehicle_count):
    # Create vehicles with predefined routes
    vehicles = [Vehicle(f"V{i}", ['A', 'B', 'C'], 0)
                for i in range(vehicle_count)]

    # Initialize metrics
    total_travel_time = 0
    reroutes = 0
    vehicles_completed = 0

    for time in range(total_simulation_time):
        # Update each signal's queue based on the light schedule
        for signal in signals.values():
            signal.process_queue(time)

        # Update vehicle positions and handle rerouting if necessary
        for vehicle in vehicles:
            vehicle.move()
            if vehicle.end_time is not None:
                total_travel_time += vehicle.end_time - vehicle.start_time
                vehicles_completed += 1
            if vehicle.rerouted:
                reroutes += 1
            # Check for congestion and reroute if needed

    # Calculate average delay per vehicle
    average_delay = total_travel_time / vehicle_count

    # Display results
    print(f"Average Delay Per Vehicle: {average_delay}")
    print(f"Total Vehicles Rerouted: {reroutes}")
    print(f"Vehicles Completed Journey: {vehicles_completed}")


def get_signal_from_network(signal_name):
    """
    Retrieves a Signal object from the network.
    This function will be part of the simulation script.
    It will take a signal name as an argument and return the corresponding Signal object from the network.
    """
    if signal_name in signal_network:
        return signal_network[signal_name]
    else:
        # Handle the case where the signal is not found in the network
        # For now, we'll just return None
        return None


def determine_direction(current_signal, next_signal):
    """
    Determines the direction to take from the current signal to the next signal.
    This function needs to be customized based on the network layout.

    This function can also be part of the simulation script, as it requires knowledge of the network layout to determine the direction.
    It will take two signal names (current and next) and return the direction the vehicle should take.
    """
    # Signal names might be like 'A1', 'B2', etc., where letter represents a row and number represents a column
    row_current, col_current = current_signal[0], current_signal[1]
    row_next, col_next = next_signal[0], next_signal[1]

    if row_current == row_next:
        if int(col_next) > int(col_current):
            return 'E'  # East
        else:
            return 'W'  # West
    else:
        if ord(row_next) > ord(row_current):
            return 'S'  # South
        else:
            return 'N'  # North


def main():
    parser = argparse.ArgumentParser(
        description='Traffic Simulation Parameters')
    parser.add_argument('--config_file', type=str,
                        help='Path to configuration file')
    args = parser.parse_args()

    if args.config_file:
        with open(args.config_file, 'r') as file:
            total_simulation_time = 1000  # Set the total simulation duration
            vehicle_count = 50  # Set the number of vehicles in the simulation
            config = json.load(file)
            signals = create_traffic_network(config)
    else:
        total_simulation_time = 1000  # Set the total simulation duration
        vehicle_count = 50  # Set the number of vehicles in the simulation
        signals = create_traffic_network()
        simulate_traffic(signals, total_simulation_time, vehicle_count)

    # Analyze and display results


if __name__ == "__main__":
    main()
