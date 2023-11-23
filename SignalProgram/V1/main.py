# Import necessary libraries
import random
from Signal_Class_02 import Signal
from Vehicle import Vehicle
from Intersection import Intersection

def create_traffic_network():
    # Create a few signals
    signals = {
        'A': Signal('A', 60),
        'B': Signal('B', 60),
        'C': Signal('C', 60)
    }
    return signals

# Function to parse the node file and create a graph
def parse_node_list(file_path):
    with open(file_path, 'r') as file:
        lines = file.readlines()

    graph = {}
    for line in lines:
        parts = line.strip().split(': ')
        node = parts[0]
        neighbors = parts[1].split(', ')
        graph[node] = neighbors

    return graph

# Simulation function
def run_simulation(node_file, vehicles, total_time):
    graph = parse_node_list(node_file)
    intersections = {node: Intersection(node, graph[node], light_cycle) for node in graph}
    time = 0

    while time < total_time:
        for vehicle in vehicles:
            vehicle.move()
        for intersection in intersections.values():
            intersection.process_queue(time)
        time += 1
    # Additional logic for simulation steps

# Result analysis function
def analyze_results(vehicles):
    total_delay = sum(v.end_time - v.start_time for v in vehicles)
    average_delay = total_delay / len(vehicles)
    # More detailed analysis as needed
    return total_delay, average_delay

# Main function
def main():
    node_file = 'path_to_node_file.txt'  # Path to the node file
    total_simulation_time = 1000  # Total simulation time in seconds
    num_vehicles = 50  # Number of vehicles in the simulation

    # Create a list of random vehicles for the simulation
    vehicles = [Vehicle(['N0', 'N1', 'N2'], 0) for _ in range(num_vehicles)]  # Example routes, modify as needed

    # Run the simulation
    run_simulation(node_file, vehicles, total_simulation_time)

    # Analyze and print the results
    total_delay, average_delay = analyze_results(vehicles)
    print(f"Total Delay: {total_delay}, Average Delay: {average_delay}")

if __name__ == "__main__":
    main()
