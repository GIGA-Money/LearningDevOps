class Signal:
    def __init__(self, throughput, act_time, reset_interval, signal_name):
        self.queues = {}
        self.throughput = throughput
        self.act_time = act_time
        self.reset_interval = reset_interval
        self.routing_map = {}
        self.signal_name = signal_name  # Identifier for the signal
        self.neighbors = {}  # Direct neighbors {neighbor_name: max_depth}
        
    def add_neighbor(self, neighbor_name, max_depth):
        """Define a direct route to a neighbor."""
        self.neighbors[neighbor_name] = max_depth
        self.queues[neighbor_name] = {'current_depth': 0, 'max_depth': max_depth}

    def get_neighbors(self):
        """Return the direct neighbors of this signal."""
        return self.neighbors.keys()
    
    def find_alternative_route(self, target, visited=None, depth=0, max_depth=3):
        """Find an alternative route to the target recursively by querying neighbors with limited depth."""
        if visited is None:
            visited = set()
        visited.add(self.signal_name)
        
        # If the current signal is the target or has a direct route to the target with no congestion
        if target in self.neighbors and self.queues[target]['current_depth'] < self.queues[target]['max_depth']:
            return [self.signal_name]
        
        # Limit the search depth
        if depth >= max_depth:
            return []
        
        # Query neighbors for alternative routes
        for neighbor_name in self.neighbors:
            if neighbor_name not in visited:
                neighbor_obj = network.signals[neighbor_name]  # Access the network's signals
                route_from_neighbor = neighbor_obj.find_alternative_route(target, visited, depth+1, max_depth)
                if route_from_neighbor:
                    return [self.signal_name] + route_from_neighbor
        return []
    
    def add_queue(self, destination, max_depth):
        """Add a queue for a specific destination."""
        self.queues[destination] = {'current_depth': 0, 'max_depth': max_depth}
        
    def add_vehicle(self, destination):
        """Add a vehicle to a queue for a particular destination."""
        if destination in self.queues:
            if self.queues[destination]['current_depth'] < self.queues[destination]['max_depth']:
                self.queues[destination]['current_depth'] += 1
            else:
                # The queue is full, need to reroute the vehicle
                pass
        else:
            # No queue for this destination, perhaps raise an exception or handle in the routing method
            pass

    def register_act_time(self, global_clock):
        """Register the next act time in the global clock."""
        next_act_time = self.act_time + self.reset_interval
        while len(global_clock) <= next_act_time:
            global_clock.append([])
        global_clock[next_act_time].append(self)