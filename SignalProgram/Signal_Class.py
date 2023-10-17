# %%
class Signal:
    def __init__(self, throughput, act_time, reset_interval):
        # List of queues {destination: (current_depth, max_depth)}
        self.queues = {}
        # Max throughput for the signal
        self.throughput = throughput
        # Time at which the signal acts
        self.act_time = act_time
        # Interval after which the signal acts again
        self.reset_interval = reset_interval
        # Routing map {destination_signal: next_signal_to_reach_destination}
        self.routing_map = {}
        
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
        
    def route_traffic(self):
        """Determine how to route the traffic based on current queue depths and the routing map."""
        pass
    
    def process_vehicles(self):
        """Process vehicles based on the throughput and update the queues."""
        pass
    
    def register_act_time(self, global_clock):
        """Register the next act time in the global clock."""
        next_act_time = self.act_time + self.reset_interval
        while len(global_clock) <= next_act_time:
            global_clock.append([])
        global_clock[next_act_time].append(self)

# Initial test for Signal object creation
signal_A = Signal(throughput=5, act_time=5, reset_interval=45)
signal_A.add_queue(destination='B', max_depth=10)
signal_A.add_vehicle(destination='B')

signal_A.queues
