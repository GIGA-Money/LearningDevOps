# %%
import matplotlib.pyplot as plt

# Data for plotting
algorithms = ["FIFO", "LRU", "Optimal", "Random"]
page_faults = [206, 187, 122, 219]
proportions = [0.707904, 0.623333, 0.406667, 1]

# Plotting Page Faults
plt.figure(figsize=(10, 6))
plt.bar(algorithms, page_faults, color=["blue", "green", "yellow", "red"])
plt.title("Total Page Faults for Different Page Replacement Algorithms")
plt.ylabel("Total Page Faults")
plt.xlabel("Algorithms")
plt.tight_layout()
plt.grid(axis="y", linestyle="--", alpha=0.7)
plt.show()

# Plotting Proportions of Page Faults to References
plt.figure(figsize=(10, 6))
plt.bar(algorithms, proportions, color=["blue", "green", "yellow", "red"])
plt.title("Proportion of Page Faults to Page References")
plt.ylabel("Proportion")
plt.xlabel("Algorithms")
plt.tight_layout()
plt.grid(axis="y", linestyle="--", alpha=0.7)
plt.show()

# %%
