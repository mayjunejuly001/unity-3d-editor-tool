# Unity 3D Editor Tool

## Overview

A lightweight **Unity editor-style 3D tool** that allows users to spawn, select, inspect, manipulate, and delete 3D objects with smooth camera navigation.  
Built using a **modular, event-driven architecture** for clean structure, good performance, and an intuitive user experience.

**Unity Version:** 2022.3.44f1 LTS

---

## Controls

### Object

- **Left Click** – Select object
- **Delete / Backspace** – Delete selected object
- **Inspector Delete Button** – Delete selected object

### Spawning

- Use the **Spawner Panel (left)** to spawn basic 3D objects
- Newly spawned objects are **auto-selected**

### Inspector

- Edit **Position (X, Y, Z)**
- Disabled automatically when no object is selected

### Camera

- **Scroll** – Zoom
- **RMB + Drag** – Rotate
- **MMB + Drag** – Pan
- **L** – Focus on selected object

---

## Additional Features

- Auto-selection of newly spawned objects
- Raycast-based deterministic selection
- Visual highlight for selected objects
- UI + keyboard deletion support
- Event-driven updates (no unnecessary polling)
- Delete Button to delete spawned objects

---

## Notes

- Selection and UI updates are event-driven
- Raycasting occurs only on user input
- Designed for clarity, performance, and extensibility
