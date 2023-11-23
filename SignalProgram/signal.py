from SignalProgram.simulation import get_signal_from_network


class Signal:
    def __init__(self, name, cycle_time, outbound_nodes):
        self.name = name
        self.cycle_time = cycle_time
        self.outbound_nodes = outbound_nodes  # Dict with node names and queue details
        # Queues for each outbound node
        self.queues = {node: [] for node in outbound_nodes}

    def get_current_light_phase(self, current_time):
        # Logic to determine the current light phase based on the cycle configuration
        # Returns a list of directions that are currently green
        cycle_position = current_time % self.cycle_length
        green_directions = []
        for direction, (start, end) in self.light_cycle.items():
            if start <= cycle_position < end:
                green_directions.append(direction)
        return green_directions

    def process_queues(self, current_time):
        green_directions = self.get_current_light_phase(current_time)
        for direction in green_directions:
            if direction in self.outbound_nodes:
                self.process_queue(self.outbound_nodes[direction], direction)

    def process_queue(self, node):
        # Process the first vehicle in the queue for the given node
        if self.queues[node]:
            vehicle = self.queues[node].pop(0)
            vehicle.move()

    def find_route_dfs(self, destination, visited=None):
        if visited is None:
            visited = set()
        visited.add(self.name)

        if destination in self.outbound_nodes and not self.queues[destination].is_full():
            return [self.name, destination]

        for neighbor in self.outbound_nodes:
            if neighbor not in visited and not self.queues[neighbor].is_full():
                neighbor_signal = get_signal_from_network(neighbor)
                route = neighbor_signal.find_route_dfs(destination, visited)
                if route:
                    return [self.name] + route
        return []

    def find_route_bfs(self, destination):
        # Initialize the queue with the current signal and its name
        queue = [(self, [self.name])]
        visited = set()  # Set to keep track of visited signals

        while queue:
            current_signal, path = queue.pop(0)  # Dequeue the first element
            visited.add(current_signal.name)

            if destination in current_signal.outbound_nodes and not current_signal.queues[destination]:
                # Destination found and queue is not congested
                return path + [destination]

            for neighbor_name in current_signal.outbound_nodes:
                if neighbor_name not in visited:
                    neighbor_signal = get_signal_from_network(neighbor_name)
                    # Enqueue the neighbor
                    queue.append((neighbor_signal, path + [neighbor_name]))

        return []  # Return an empty list if no route is found

    def add_vehicle_to_queue(self, vehicle, destination):
        if destination in self.queues:
            self.queues[destination].append(vehicle)
        else:
            # Attempt to reroute the vehicle
            alternative_route = self.find_route_dfs(vehicle.route[-1])
            if not alternative_route:
                alternative_route = self.find_route_bfs(vehicle.route[-1])

            if alternative_route:
                vehicle.update_route([self.name] + alternative_route)
            else:
                # If rerouting fails, delay the vehicle at the current signal
                # This could be represented by adding the vehicle to a special 'delayed' queue
                self.queues.get('delayed', []).append(vehicle)
