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
      
    def register_act_time(self, global_clock):
        """Register the next act time in the global clock."""
        next_act_time = self.act_time + self.reset_interval
        while len(global_clock) <= next_act_time:
            global_clock.append([])
        global_clock[next_act_time].append(self)
        
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