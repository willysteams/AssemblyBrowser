using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyBrowserCore;

namespace AssemblyBrowserGui;

public sealed class ApplicationViewModel : INotifyPropertyChanged
{
    private readonly Model _model;
    private ObservableCollection<IElementInfo> _namespaces;
    private readonly OpenAssemblyDialog _openAssemblyDialog;
    
    public ObservableCollection<IElementInfo> Namespaces
    {
        get => _namespaces;
        private set
        {
            _namespaces = value;
            OnPropertyChanged();
        }
    }
    
    private RelayCommand? _loadNewAssemblyCommand;
    public RelayCommand LoadNewAssemblyCommand
    {
        get
        {
            return _loadNewAssemblyCommand ??= new RelayCommand(obj =>
            {
                try
                {
                    if (!_openAssemblyDialog.OpenFileDialog()) return;
                    
                    _model.UpdateNamespace(_openAssemblyDialog.FilePath);
                    Namespaces = _model.Namespaces;
                }
                catch (Exception ex)
                {
                    OpenAssemblyDialog.ShowMessage(ex.Message);
                }
                
            });
        }
    }

    public ApplicationViewModel()
    {
        _model = new Model();
        _openAssemblyDialog = new OpenAssemblyDialog();
        _namespaces = new ObservableCollection<IElementInfo>();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}