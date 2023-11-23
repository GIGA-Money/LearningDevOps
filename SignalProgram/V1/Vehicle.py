from Intersection import Intersection
# Vehicle Class
class Vehicle:
    def __init__(self, route, start_time):
        self.route = route
        self.current_node = route[0]
        self.start_time = start_time
        self.end_time = None
        #self.identifier = 

    def update_current_node(self, new_node):
        self.current_node = new_node
    
    def move(self, intersections):
        """ 
        The move method in the Vehicle class should interact with the Intersection class. 
        Currently, it seems to try to access a method add_vehicle_to_queue which might not exist in your setup. 
        This needs to be aligned with the logic in the Intersection class.
        """
        if self.route:
            next_node = self.route.pop(0)
            self.current_node = next_node  # Update current node
            direction = self.determine_direction(next_node)  # Determine direction based on route
            intersections[next_node].add_vehicle_to_queue(self, direction)

    def determine_direction(self, next_intersection):
