function showStep(step) {
    // Hide all steps first
    for (let i = 1; i <= 4; i++) {
        document.getElementById('step' + i).style.display = 'none';
    }
    document.getElementById('step' + step).style.display = 'block';

    // Update progress bar and heading based on the step
    if (step === 2) {
        updateProgress(50);
        document.querySelector('.progress-heading h3').textContent = 'Company Insights';
    } else if (step === 3) {
        updateProgress(75);
        document.querySelector('.progress-heading h3').textContent = 'Company Departments Information';
        var form = document.getElementsByClassName('roles-form')[0];
        var roleInputs = document.getElementsByClassName('role-inputs')[0];

        // Repeat the code 9 times
        for (var i = 0; i < 9; i++) {
            var newRoleInput = document.createElement('div');
            newRoleInput.className = 'form-row';

            newRoleInput.innerHTML = `
        <div class="form-group col-md-6">
            <label for="role">Role</label>
            <input type="text" class="form-control" name="Role${i + 1}" placeholder="Enter role" required>
        </div>
        <div class="form-group col-md-6">
            <label for="salary">Salary</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text">$</span>
                </div>
                <input type="number" class="form-control" name="Salary${i + 1}" placeholder="Enter salary" required>
                <div class="input-group-append">
                    <span class="input-group-text">.00</span>
                </div>
            </div>
        </div>
    `;

            roleInputs.appendChild(newRoleInput);
        }


    } else if (step === 4) {
        updateProgress(100);
        document.querySelector('.progress-heading h3').textContent = 'Average Salary Growth Rate(per month) for the Last 12 Months (excluding the current month)';
        for (var i = 1; i <= 12; i++) {
            var month = new Date();
            month.setMonth(month.getMonth() - i);
            var monthName = month.toLocaleString('default', { month: 'long' });
            var year = month.getFullYear();

            var salaryInputsContainer = document.getElementById('salaryInputsContainer');

            var formRow = document.createElement('div');
            formRow.className = 'form-row';

            var formGroup = document.createElement('div');
            formGroup.className = 'form-group col-md-6';

            var label = document.createElement('label');
            label.setAttribute('for', 'salary-' + i);
            label.textContent = 'Salary for ' + monthName + ' ' + year;

            var input = document.createElement('input');
            input.setAttribute('type', 'number');
            input.setAttribute('class', 'form-control');
            input.setAttribute('id', 'salary-' + i);
            input.setAttribute('placeholder', 'Enter salary');
            input.setAttribute('required', 'true');

            formGroup.appendChild(label);
            formGroup.appendChild(input);
            formRow.appendChild(formGroup);

            salaryInputsContainer.appendChild(formRow);
        }

    }
}

function updateProgress(completeness) {
    const progressBar = document.querySelector('.progress-bar');
    progressBar.style.width = completeness + '%';
    progressBar.textContent = completeness + '% Complete';
}

function completeProfile() {
    // Add logic to handle the completion of the profile, e.g., submit the form data.
    alert('Profile completed successfully!');
}
function validateAndShowStep(nextStep, currentStepId) {
    // Validate required fields in the current step
    showStep(nextStep)
    var currentStep = document.getElementById(currentStepId);
    var requiredInputs = currentStep.querySelectorAll('[required]');

    for (var i = 0; i < requiredInputs.length; i++) {
        if (requiredInputs[i].value.trim() === '') {
            alert('Please fill in all required fields.');
            return; // Stop further execution
        }
        //
    }

    // Hide all steps
    for (var i = 1; i <= 4; i++) {
        document.getElementById('step' + i).style.display = 'none';
    }

    // Show the next step
    document.getElementById('step' + nextStep).style.display = 'block';
}