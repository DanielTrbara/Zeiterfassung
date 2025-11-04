function addTimeEntry() {
    const startTime = document.getElementById('start-time').value;
    const endTime = document.getElementById('end-time').value;
    const description = document.getElementById('description').value;

    if (!startTime || !endTime) {
        alert('Please enter both start and end time!');
        return;
    }

    // Berechne Stunden
    const start = new Date('2000-01-01 ' + startTime);
    const end = new Date('2000-01-01 ' + endTime);
    const diffMs = end - start;
    const diffHrs = diffMs / (1000 * 60 * 60);

    // Kategorie basierend auf Beschreibung
    let category = 'working-time';
    let categoryText = 'Working time';

    if (description.toLowerCase().includes('break')) {
        category = 'break-time';
        categoryText = 'Break time';
    } else if (description.toLowerCase().includes('overtime')) {
        category = 'overtime';
        categoryText = 'Overtime';
    }

    // Neue Zeile erstellen
    const tbody = document.getElementById('time-entries');
    const newRow = document.createElement('tr');
    newRow.className = category;
    newRow.innerHTML = `
        <td>${categoryText}</td>
        <td>${startTime} - ${endTime}</td>
        <td>${diffHrs.toFixed(1)} Hours</td>
    `;

    tbody.appendChild(newRow);

    // Formular zurücksetzen
    document.getElementById('start-time').value = '';
    document.getElementById('end-time').value = '';
    document.getElementById('description').value = '';

    // Total aktualisieren
    updateTotal();
}

function updateTotal() {
    const rows = document.querySelectorAll('#time-entries tr');
    let total = 0;

    rows.forEach(row => {
        const hoursText = row.cells[2].textContent;
        const hours = parseFloat(hoursText);
        total += hours;
    });

    document.getElementById('total-hours').textContent = total.toFixed(1) + ' hours';
}

//Gespeicherte Einträge im Controller


