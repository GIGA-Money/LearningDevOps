class Vehicle:
    def __init__(self, route, start_time):
        self.route = route
        self.current_node = route[0]
        self.destination = route[-1]
        self.start_time = start_time
        self.end_time = None

    def move(self):
        # Logic to move to the next node in the route
        pass

    def update_route(self, new_route):
        # Logic to update the vehicle's route
        pass
