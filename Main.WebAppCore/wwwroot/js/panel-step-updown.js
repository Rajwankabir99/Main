
const listPanel = document.querySelector('.list-panel');
let selectedPanel = null;
const panelOrder = [];


function confirmAndProceed()
{

    // 1. Display the native confirmation box
    const userConfirmed = confirm ("Are you sure you want to delete this Panel?");

    // 2. Evaluate the user's choice
    if (userConfirmed)
    {
        console.log("User clicked OK (Yes). Proceeding with action...");
        // Call your delete logic here

        return true;
    }
    else
    {
        console.log("User clicked Cancel (No). Action aborted.");

        return false;
    }
}


// Initialize panel order array
async function initializePanelOrder()
{
    const panels = Array.from(listPanel.querySelectorAll('.panel'));

    panelOrder.length = 0;

    panels.forEach((panel, index) => {

        panelId = panel.id;

        panelOrder.push({ "PanelID": panelId, "PanelPosition": index });
    });
}

// Setup event listeners for all panels
async function setupPanels()
{
    const panels = listPanel.querySelectorAll('.panel');

    panels.forEach(panel =>
    {
        // Single click to select
        panel.addEventListener('click', async (e) => {
            if (e.detail !== 2)
            { // Ignore double-clicks
                await selectPanel(panel);
            }
        });

        // Double-click to unselect
        panel.addEventListener('dblclick', unselectPanel);
    });
}

// Select a panel
async function selectPanel(panel)
{
    // Deselect previous panel
    if (selectedPanel)
    {
        selectedPanel.classList.remove('selected');
        removeArrows();
    }

    // Select new panel
    selectedPanel = panel;
    selectedPanel.classList.add('selected');

    // Add arrow buttons
    await addArrows();

    // Scroll to middle of screen
    scrollToMiddle();
}

// Scroll selected panel to middle of screen
function scrollToMiddle()
{
    if (!selectedPanel) return;

    selectedPanel.scrollIntoView({
        behavior: 'smooth',
        block: 'center'
    });
}





// Add up and down arrow buttons to selected panel
async function addArrows()
{
    const existingArrows = selectedPanel.querySelector('.arrow-buttons');

    if (existingArrows)
        return;

    const arrowContainer = document.createElement('div');
    arrowContainer.className = 'arrow-buttons';

    // Up arrow button
    const upArrowBtn = document.createElement('button');
    upArrowBtn.className = 'arrow-btn up-arrow-btn';
    upArrowBtn.innerHTML = '↑';
    upArrowBtn.title = 'Move up';
    upArrowBtn.setAttribute('aria-label', 'Move up');

    upArrowBtn.addEventListener('click', async (e) =>
    {
        e.stopPropagation();
        await moveUp();
    });

    // Down arrow button
    const downArrowBtn = document.createElement('button');
    downArrowBtn.className = 'arrow-btn down-arrow-btn';
    downArrowBtn.innerHTML = '↓';
    downArrowBtn.title = 'Move down';
    downArrowBtn.setAttribute('aria-label', 'Move down');

    downArrowBtn.addEventListener('click', async (e) =>
    {
        e.stopPropagation();
        await moveDown();
    });

    // Save button
    const saveBtn = document.createElement('button');
    saveBtn.className = 'save-btn';
    saveBtn.innerHTML = 'Save';
    saveBtn.title = 'Save or Press Enter';
    saveBtn.setAttribute('aria-label', 'Save or Press Enter');

    saveBtn.addEventListener('click', async (e) =>
    {
        e.stopPropagation();
       
        await savePanelOrders();
    });

    // Delete button
    const deleteBtn = document.createElement('button');
    deleteBtn.className = 'delete-btn';
    deleteBtn.id = selectedPanel.id;
    deleteBtn.innerHTML = 'Delete';
    deleteBtn.title = 'Click to Delete Panel';
    deleteBtn.setAttribute('aria-label', 'Click to Delete Panel');

    deleteBtn.addEventListener('click', async (e) =>
    {
        e.stopPropagation();

        var id = e.target.id

        if (confirmAndProceed())
        {
            await deletePanel(id);
        }
    });

    arrowContainer.appendChild(deleteBtn);
    arrowContainer.appendChild(saveBtn);
    arrowContainer.appendChild(upArrowBtn);
    arrowContainer.appendChild(downArrowBtn);
    
    selectedPanel.appendChild(arrowContainer);

    // Update arrow visibility
    updateArrowVisibility();
}

async function deletePanel(id)
{
    if (!selectedPanel)
        return;

    console.log("Delete Panel Id:" + id);

    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');

    if (!tokenElement) {
        console.error("Anti-forgery token element not found in the DOM.");
        return false;
    }

    const tokenValue = tokenElement.value;

    try
    {

        const response =

            await fetch( '@Url.Action("DeletePanel", "Pages", new { area = "PageContent" })' +  id ,
            {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': tokenValue
                }
                });

        const dataresponse = await response.json();

        consoe.log(dataresponse);

        if (dataresponse.success)
        {
            const element = document.getElementById(id);

            if (element)
            {
                element.remove();
                await unselectPanel();

                return true;
            }
        }
        else if (response.status === 404)
        {
            console.error('The item could not be found on the server.');
        }
        else
        {
            console.error('An error occurred while trying to delete the item.');
        }
    }
    catch (error)
    {
        console.error('Network error or request failure:', error);
    }

    return false;
}

async function GetPanelPositionData()
{
    return await updatePanelOrder();
}

async function savePanelOrders()
{

    const data = await GetPanelPositionData();

    const urlUpdateOprders = '@Url.Action("UpdatePositions", "Pages", new { area = "PageContent" })';
    
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');

    if (!tokenElement)
    {
        console.error("Anti-forgery token element not found in the DOM.");
        return false;
    }

    const tokenValue = tokenElement.value;

    try
    {
        const response =

            await fetch( urlUpdateOprders ,
            {
                method: 'POST' ,
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': tokenValue
                },
                body: JSON.stringify(data)
            } );

        const dataresponse = await response.json();

        if (dataresponse.success)
        {
            await unselectPanel();

            console.log("Success:", dataresponse.success);
        }
        else
        {
            console.error("Server Error:", dataresponse.success, dataresponse.error);
        }
    }
    catch (error)
    {
        console.error("Network Error:", error);
    }

}

// Remove arrow buttons
function removeArrows()
{
    const arrowContainer = selectedPanel.querySelector('.arrow-buttons');

    if (arrowContainer)
        arrowContainer.remove();
}

// Update arrow button visibility based on position
function updateArrowVisibility()
{
    if (!selectedPanel)
        return;

    const panels = Array.from(listPanel.querySelectorAll('.panel'));
    const currentIndex = panels.indexOf(selectedPanel);
    const isFirst = currentIndex === 0;
    const isLast = currentIndex === panels.length - 1;

    const upArrowBtn = selectedPanel.querySelector('.up-arrow-btn');
    const downArrowBtn = selectedPanel.querySelector('.down-arrow-btn');

    // Hide/disable up arrow if first
    if (upArrowBtn)
    {
        if (isFirst) {
            upArrowBtn.classList.add('disabled');
            upArrowBtn.disabled = true;
        }
        else
        {
            upArrowBtn.classList.remove('disabled');
            upArrowBtn.disabled = false;
        }
    }

    // Hide/disable down arrow if last
    if (downArrowBtn)
    {
        if (isLast)
        {
            downArrowBtn.classList.add('disabled');
            downArrowBtn.disabled = true;
        }
        else
        {
            downArrowBtn.classList.remove('disabled');
            downArrowBtn.disabled = false;
        }
    }
}

// Move selected panel up one step
async function moveUp()
{
    if (!selectedPanel)
        return;

    const panels = Array.from(listPanel.querySelectorAll('.panel'));

    const currentIndex = panels.indexOf(selectedPanel);

    if (currentIndex > 0) {
        const prevPanel = selectedPanel.previousElementSibling;

        listPanel.insertBefore(selectedPanel, prevPanel);

        // Update order array and arrow visibility
        await updatePanelOrder();
        updateArrowVisibility();

        // Keep panel centered on screen after move
        scrollToMiddle();
    }
}


// Move selected panel down one step
async function moveDown()
{
    if (!selectedPanel)
        return;

    const panels = Array.from(listPanel.querySelectorAll('.panel'));

    const currentIndex = panels.indexOf(selectedPanel);

    if (currentIndex < panels.length - 1)
    {
        const nextPanel = selectedPanel.nextElementSibling;
        listPanel.insertBefore(nextPanel, selectedPanel);

        // Update order array and arrow visibility
        await updatePanelOrder();
        updateArrowVisibility();

        // Keep panel centered on screen after move
        scrollToMiddle();
    }
}

// Update panel order array 
 async function updatePanelOrder()
{
    const panels = Array.from(listPanel.querySelectorAll('.panel'));

    panelOrder.length = 0;

    panels.forEach((panel, index) => {
        
        panelId = panel.id;

        panelOrder.push({ "PanelID": panelId, "PanelPosition": index });
    });

    console.log('Current panel order:', panelOrder);
}

// Unselect panel
async function unselectPanel()
{
    if (selectedPanel)
    {
        selectedPanel.classList.remove('selected');
        await removeArrows();
        selectedPanel = null;
    }
}


// Listen for Enter key to unselect
document.addEventListener('keydown', async (e) =>
{
    if (e.key === 'Enter' && selectedPanel) {
       await unselectPanel();
    }
});

// Initialize on DOM ready
document.addEventListener('DOMContentLoaded', async () =>
{
    await initializePanelOrder();

    await setupPanels();
});