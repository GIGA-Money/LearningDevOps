from Vechicle import Vehicle

# Intersection Class
class Intersection:
    def __init__(self, name, outbound_queues, light_cycle):
        self.name = name
        self.outbound_queues = {queue: [] for queue in outbound_queues}  # Empty queues
        self.light_cycle = light_cycle  # Dict with time ranges for each direction

    def add_vehicle_to_queue(self, vehicle, direction):
        if direction in self.outbound_queues:
            self.outbound_queues[direction].append(vehicle)
    
    def process_queue(self, current_time):
        """
        determine the current phase of the light cycle based on the current time.
        process vehicules in the queue(s) that are active during teh phase.
        move vehicles to their next destination, considering queue sizes and congestion.
        @param: current_time
        """
        for direction, queue in self.outbound_queues.items():
            if self.is_green_light(direction, current_time):
                self.process_direction_queue(queue, direction)

    # Additional methods for rerouting, congestion checking, etc.
    def determine_current_phase(self, current_time):
        """
        logic to determine which part of the light cycle is active 
        this will return a list of directions that are currently active

        * the current time will be a running counter that represents the simulation time, typically incremented in a loop within the main funciton.
        * current tiem is passed to the process queue method of each intersection object which in turn calls determin_current_phase.
        * this method caclulates which pase, which directions are currently green, is active based on the current time.

        @param current time
        @return list of directions
        """

    def process_direction_queue(self, queue, direction):
        """
        logic to process vehicles in a specific direction queue
        might involve moving vehiclaes to the next node in their route
        * needs to interact with the 'vehicle' class, updaing the position of vehiciles and possibly recalculating their routes if congestion is detected.
        * ensure the light cycle tming is correctly handled. for example if the light cycle is 60 seconds and the north-south direction is active for the first 30 seconds, the 
        *   method needs to correctly identify and process the north-south queue during that time.
        @params queue, direction
        """
        while queue and self.is_green_light(direction):
            vehicle = queue.pop(0)
            vehicle.move()

    def is_green_light(self, direction, current_time):
        # Determine if the light is green for a specific direction
        # return bool

    def can_move(self, vehicle, direction):
        # Determine if a vehicle can move in a specific direction
        # return bool, might not need this method