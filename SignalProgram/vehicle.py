from SignalProgram.simulation import determine_direction, get_signal_from_network


class Vehicle:
    def __init__(self, identifier, route, start_time):
        self.identifier = identifier
        self.route = route  # List of signal names representing the route
        self.current_position = 0  # Index in the route list
        self.start_time = start_time
        self.end_time = None
        self.current_signal = None

    def move(self):
        if self.current_position < len(self.route) - 1:
            next_signal_name = self.route[self.current_position + 1]
            next_signal = get_signal_from_network(next_signal_name)
            if not next_signal.is_queue_full(self.get_next_direction()):
                next_signal.add_vehicle_to_queue(
                    self, self.get_next_direction())
                self.current_position += 1
            else:
                self.reroute(next_signal)
        else:
            self.end_time = get_current_simulation_time()

    def get_next_direction(self):
        # Determine the direction based on the current and next signal
        return determine_direction(self.current_signal.name, self.route[self.current_position + 1])

    def reroute(self, congested_signal):
        alternative_route = congested_signal.find_route_dfs(self.route[-1])
        if not alternative_route:
            alternative_route = congested_signal.find_route_bfs(self.route[-1])

        if alternative_route:
            self.route = self.current_signal.name + alternative_route
        else:
            # Handle the case where no alternative route is found
            pass
