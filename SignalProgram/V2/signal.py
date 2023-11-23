"""
Signal Class
The Signal class will have the following responsibilities:
Managing queues of vehicles.
Handling the addition and removal of vehicles from queues.
Rerouting vehicles when necessary.
Communicating with neighboring signals to find alternative routes.

Signal Class Updates
Queue Management: Each Signal should manage multiple queues corresponding to its outbound directions. These queues will store vehicles waiting to move in each direction.

Light Cycle Logic: Implement logic to process queues based on the current time in the light cycle. This involves:

Determining the active phase of the light cycle based on the current time.
Processing vehicles in the queue(s) that are active during this phase.
Rerouting Logic: Implement logic to reroute vehicles when the next node in their route is congested.
"""
from intersection import intersections
class Signal:
    def __init__(self, intersections):
        self.signal_name = signal_name
        self.queues = {}  # Queues for each direction
        self.neighbors = []  # Neighboring signals
    
    def add_queue(self, direction, max_depth):
        """Initialize a queue for a specific direction with a maximum depth."""
        self.queues[direction] = {'current_depth': 0, 'max_depth': max_depth}
        
    def add_neighbor(self, neighbor_signal):
        """Add a neighboring signal to facilitate rerouting."""
        self.neighbors.append(neighbor_signal)
    
    def add_vehicle(self, vehicle, direction):
        """Add a vehicle to the queue for the specified direction."""
        if self.queues[direction]['current_depth'] < self.queues[direction]['max_depth']:
            self.queues[direction]['current_depth'] += 1
            vehicle.current_signal = self
        else:
            self.reroute_vehicle(vehicle, direction)

    def reroute_vehicle(self, vehicle):
        # Logic for rerouting a vehicle
        pass

    def check_congestion(self, intersection):
        # Logic to check congestion at an intersection
        pass

    def is_congested(self):
        """ Check if the signal is congested. """
        # This can be a simple check of the vehicle count in the queue vs. a congestion threshold.
        # Implementation depends on how congestion is defined in your system.
        pass

    def find_route_dfs(self, destination, visited=None, max_depth=3):
        """Find a route to the destination using DFS, with a limited depth."""
        if visited is None:
            visited = set()
        visited.add(self.signal_name)

        if self.signal_name == destination:
            return [self.signal_name]

        if len(visited) > max_depth:
            return None

        for neighbor in self.neighbors:
            if neighbor.signal_name not in visited:
                route = neighbor.find_route_dfs(destination, visited, max_depth)
                if route:
                    return [self.signal_name] + route
        return None

    def find_route_bfs(self, destination, max_depth=3):
        """Find a route to the destination using BFS, with a limited depth."""
        queue = [(self, [self.signal_name])]
        visited = set()

        while queue:
            current_signal, path = queue.pop(0)
            if current_signal.signal_name == destination:
                return path

            if len(path) <= max_depth + 1:
                for neighbor in current_signal.neighbors:
                    if neighbor.signal_name not in visited:
                        visited.add(neighbor.signal_name)
                        queue.append((neighbor, path + [neighbor.signal_name]))
        return None