# Rationale

Why semantic quantities?

- Dimensional safety: Using a dimensional signature (L, M, T, I, Θ, N, J, Angle) helps prevent invalid operations (e.g., adding meters to seconds) and provides meaningful results for products/quotients (e.g., m/s, N = kg·m/s²).
- Consistent conversions: Storing values in coherent SI internally means conversions are centralized and reliable. Display units can differ without affecting internal correctness.
- Angle as a base dimension: Treating angle as a distinct base quantity avoids accidental mixing with pure numbers and fits domains (astronomy, rotations) where angles are foundational.
- Registry-based unit system: A registry maps units to their systems (SI, Imperial). When combining quantities from different systems, results default to SI, ensuring predictability and avoiding silent unit drift.
- Extensibility: New units can be registered without modifying core types (open-closed principle). Compound units are supported via a parser and canonical symbol construction.

Trade-offs and decisions:
- Runtime Quantity over generic type-parameter quantities simplifies scenarios that need late-bound units, parsing, or dynamically combining units. A legacy generic remains for migration.
- Offsets vs. scales: Most units are scale-only; temperature or other affine units would require careful handling. The design anticipates this, though most current units are linear.
- Time handling separates concerns via TimeInstant<TScale> with context-driven conversions (e.g., leap seconds), rather than baking such rules into generic quantity arithmetic.
