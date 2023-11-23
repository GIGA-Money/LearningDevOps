from SignalProgram.simulation import determine_direction, get_signal_from_network


class Vehicle:
    def __init__(self, identifier, route, start_time):
        self.identifier = identifier
        self.route = route
        self.current_position = 0
        self.start_time = start_time
        self.end_time = None
        self.current_signal = None

    def move(self):
        if self.current_position < len(self.route) - 1:
            self.current_position += 1  # Simply update the position
        else:
            self.end_time = get_current_simulation_time()

    def get_next_direction(self):
        return determine_direction(self.current_signal.name, self.route[self.current_position + 1])

    def reroute(self, congested_signal):
        # Rerouting logic remains the same
        alternative_route = congested_signal.find_route_dfs(self.route[-1])
        if not alternative_route:
            alternative_route = congested_signal.find_route_bfs(self.route[-1])

        if alternative_route:
            self.route = self.current_signal.name + alternative_route
        else:
            # Handle the case where no alternative route is found
            pass
