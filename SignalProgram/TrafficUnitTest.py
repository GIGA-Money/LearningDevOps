import unittest
from Signal import Signal
from vehicle import Vehicle


class TestSignal(unittest.TestCase):

    def setUp(self):
        self.signal = Signal('A', 60, {'B': 'N', 'C': 'E'}, {
                             'N': 10, 'E': 10, 'S': 10, 'W': 10})
        self.signal.update_light_schedule(
            {'N': (0, 15), 'E': (15, 30), 'S': (30, 45), 'W': (45, 60)})

    def test_process_queue(self):
        # Example: Add vehicle, simulate time, check if vehicle is processed
        vehicle = Vehicle("V1", ["A", "B"], 0)
        self.signal.add_vehicle_to_queue(vehicle, 'N')
        self.signal.process_queue(5)  # Time within 'N' light phase
        # Check if the vehicle was processed (queue size reduced)
        self.assertEqual(
            len(self.signal.queues['N']), 0, "Vehicle should be processed")

    def test_add_vehicle_to_queue(self):
        vehicle = Vehicle("V2", ["A", "C"], 0)
        self.signal.add_vehicle_to_queue(vehicle, 'E')
        # Check if the vehicle is in the queue
        self.assertIn(
            vehicle, self.signal.queues['E'], "Vehicle should be in 'E' queue")

    # Additional test cases here...


if __name__ == '__main__':
    unittest.main()
