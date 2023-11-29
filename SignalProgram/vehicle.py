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
        # Modified move method with dynamic decision-making
        current_signal = get_signal_from_network(self.current_signal.name)
        next_signal_name = self.route[self.current_position + 1]

        # Get congestion information and route suggestions
        suggested_route = current_signal.suggest_route(next_signal_name)

        if not next_signal.is_queue_full(self.get_next_direction()):
            next_signal.add_vehicle_to_queue(self, self.get_next_direction())
            self.current_position += 1
        else:
            # Handling congestion
            self.handle_congestion()

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

    def handle_congestion(self):
        current_signal = get_signal_from_network(
            self.route[self.current_position])
        suggested_route = current_signal.suggest_route(self.route[-1])

        # Update the route with the suggested route
        self.update_route(suggested_route)

    def update_route(self, new_direction):
        # Logic to update the route of the vehicle
        # This may involve changing the next destination in the route
        self.route[self.current_position + 1] = new_direction
