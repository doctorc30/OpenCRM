using System;
using System.Data.Entity;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;

namespace bbom.Data.Repository
{
    public class ContextMenager
    {
        private DbContext _ic;
        private DbContext _cc;
        private DbContext _current;
        public ContextMenager()
        {
            _ic = new identityDbEntities();
            _cc = new ContentDbEntities();
        }
        public DbContext GetContext<TEntity>()
        {
            if (typeof(AspNetUser) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(AspNetRole) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(AspNetUserClaim) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(AspNetUserLogin) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(Communicatio) == typeof(TEntity))
            {
                return _ic;
            }
            if (typeof(Discount) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(DiscountType) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(Payment) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(PaymentPlan) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(UserCommunication) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(UserInvitedDiscount) == typeof(TEntity))
            {
                _current = _ic;
                return _ic;
            }
            if (typeof(AccessToEventType) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(AccessToMenu) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(DefaultSettingsValue) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(Event) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(EventSpiker) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(EventType) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(ExtraRegParam) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(Menu) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(ReceivedExtraRegParam) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(Setting) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(TaskToDo) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(Template) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(UserComplitedTask) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(UserExtraRegParam) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(UsersTemplateSetting) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(PostType) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(Post) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(Registrator) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            if (typeof(RegistratorSetting) == typeof(TEntity))
            {
                _current = _cc;
                return _cc;
            }
            //if (_current != null)
            //{
            //    return _current;
            //}
            //var properties = _ic.GetType().GetProperties().Select(p => p.PropertyType);
            //if (properties.Contains(typeof(TEntity)))
            //{
            //    _current = _ic;
            //    return _ic;
            //}
            //properties = _cc.GetType().GetProperties().Select(p => p.PropertyType);
            //if (properties.Contains(typeof(TEntity)))
            //{
            //    _current = _cc;
            //    return _cc;
            //}
            throw new Exception("Таблица не принадлежит БД");
        }
    }
}